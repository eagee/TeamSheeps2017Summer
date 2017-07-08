using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    private float initSpeed = 1.0f;
    private float speed = 1.0f;
    private float acceleration = 0.2f;
    private float maxSpeed = 3.2f;
    public float moveCameraDirection = 0f;

    public GameObject[] backgrounds;
    public int currBackground;
    public GameObject background;

    [HideInInspector]
    public bool moveCamera;

    // Use this for initialization
    void Start () {
        SpriteRenderer srend;
        GameObject new_bg;

        Debug.Log("Starting camera Start()");
        Vector3 bg_position = transform.position;
        bg_position.z = 0f;

        // for (int i = 0; i < backgrounds.Length; i++) {
        for (int i = 0; i < 1; i++) {
            new_bg = Instantiate(backgrounds[i], bg_position, Quaternion.identity);
            backdrop bdScript = new_bg.GetComponent<backdrop>();
            if (bdScript) bdScript.bdNumber = i;
            srend = new_bg.GetComponent<SpriteRenderer>();
            bg_position.x += srend.bounds.extents.x;
            new_bg.transform.position = bg_position;
            bg_position.x = srend.bounds.max.x;
        }

        Debug.Log("Ending camera Start()");
    }

    void Update() {
        if (moveCameraDirection != 0f && Input.GetKey("space")) {
            moveCamera = false;
            moveCameraDirection = 0f;
        } else if (moveCameraDirection != 1f && Input.GetKey("right")) {
            moveCamera = true;
            moveCameraDirection = 1f;
            speed = initSpeed;
        } else if (moveCameraDirection != -1f && Input.GetKey("left")) {
            moveCamera = true;
            moveCameraDirection = -1f;
            speed = initSpeed;
        }
        if (moveCamera) {
            MoveCamera();
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit " + other.name, gameObject);
        backdrop bdScript = other.GetComponent<backdrop>();
        Debug.Log("bdScript " + bdScript);
        if (bdScript) {
            bdScript.hasCamera = false;
        }
    }

    // Note that we will trigger enter on a newly instantiated background before it has completed its "Start".
    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter " + other.name, gameObject);
        backdrop bdScript = other.GetComponent<backdrop>();
        Debug.Log("bdScript " + bdScript);
        if (bdScript) {
            bdScript.hasCamera = true;
            // Make sure I have a previous and next backdrop
            if (!bdScript.next) {
                // find the right edge
                SpriteRenderer srendCurrent = other.GetComponent<SpriteRenderer>();
                Vector3 newPosition = other.transform.position;
                newPosition.x = srendCurrent.bounds.max.x;
                // Instantiate the next backdrop
                int newBdNumber = bdScript.bdNumber + 1;
                if (newBdNumber >= backgrounds.Length) newBdNumber = 0;
                GameObject newBackdrop = Instantiate(backgrounds[newBdNumber], newPosition, Quaternion.identity);
                // Move it half its width to the right to line up
                SpriteRenderer srendNew = newBackdrop.GetComponent<SpriteRenderer>();
                newPosition.x += srendNew.bounds.extents.x;
                newBackdrop.transform.position = newPosition;
                // Now assign the number and neighborly links
                backdrop newBdScript = newBackdrop.GetComponent<backdrop>();
                newBdScript.bdNumber = newBdNumber;
                bdScript.next = newBackdrop;
                newBdScript.prev = other.gameObject;
            }
            if (!bdScript.prev) {
                // find the left edge
                SpriteRenderer srendCurrent = other.GetComponent<SpriteRenderer>();
                Vector3 newPosition = other.transform.position;
                newPosition.x = srendCurrent.bounds.min.x;
                // Instantiate the next backdrop
                int newBdNumber = bdScript.bdNumber - 1;
                if (newBdNumber < 0) newBdNumber = backgrounds.Length - 1;
                GameObject newBackdrop = Instantiate(backgrounds[newBdNumber], newPosition, Quaternion.identity);
                // Move it half its width to the right to line up
                SpriteRenderer srendNew = newBackdrop.GetComponent<SpriteRenderer>();
                newPosition.x -= srendNew.bounds.extents.x;
                newBackdrop.transform.position = newPosition;
                // Now assign the number and neighborly links
                backdrop newBdScript = newBackdrop.GetComponent<backdrop>();
                newBdScript.bdNumber = newBdNumber;
                bdScript.prev = newBackdrop;
                newBdScript.next = other.gameObject;
            }

        }
    }

    void MoveCamera() {

        Vector3 temp = transform.position;

        temp.x = temp.x + (moveCameraDirection * speed * Time.deltaTime);

        transform.position = temp;

        speed += acceleration * Time.deltaTime;

        if (speed > maxSpeed)
            speed = maxSpeed;

    }
}
