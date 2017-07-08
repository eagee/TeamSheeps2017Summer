using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    private float initSpeed = 1.0f;
    private float speed = 1.0f;
    private float acceleration = 0.2f;
    private float maxSpeed = 3.2f;
    public float moveCameraDirection = 0f;

    public int initialBackdrop;
    public GameObject[] backgrounds;
    public GameObject[] toys;

    [HideInInspector]
    public bool moveCamera;

    // Smooth camera stuff
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera camera;
    public Vector3 initialPosition;

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;
        camera = GetComponent<Camera>();

        GameObject new_bg, new_toy;

        // Initial background at the same position as camera, but z of 0f.
        Vector3 bg_position = transform.position;
        bg_position.z = 0f;
        Vector3 toy_position = transform.position;
        toy_position.z = 0f;

        new_bg = Instantiate(backgrounds[initialBackdrop], bg_position, Quaternion.identity);
        backdrop bdScript = new_bg.GetComponent<backdrop>();
        if (bdScript) bdScript.bdNumber = initialBackdrop;
        new_toy = Instantiate(toys[initialBackdrop], toy_position, Quaternion.identity);
        toy toyScript = new_toy.GetComponent<toy>();
        toyScript.backdrop = new_bg;
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
        } else {
            SmoothFollowCamera();
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit " + other.name, gameObject);
        backdrop bdScript = other.GetComponent<backdrop>();
        Debug.Log("bdScript " + bdScript);
        if (bdScript) {
            bdScript.hasCamera = false;
            // If I have a neighbor not on camera, destroy it.
        //    if (bdScript.prev && !bdScript.prev.GetComponent<backdrop>().hasCamera) {
        //        GameObject destroyMe = bdScript.prev;
        //        bdScript.prev = null;
        //        Debug.Log("I want to destroy (prev) " + destroyMe);
        //        destroyMe.GetComponent<backdrop>().destroyMyself = true;
        //    }
        //    if (bdScript.next && !bdScript.next.GetComponent<backdrop>().hasCamera) {
        //        GameObject destroyMe = bdScript.next;
        //        bdScript.next = null;
        //        Debug.Log("I want to destroy (next) " + destroyMe);
        //        destroyMe.GetComponent<backdrop>().destroyMyself = true;
        //    }
        }
    }

    // Note that we will trigger enter on a newly instantiated background before it has completed its "Start".
    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter " + other.name, gameObject);
        backdrop bdScript = other.GetComponent<backdrop>();
        Debug.Log("bdScript " + bdScript);
        GameObject new_toy;
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
                GameObject newBackdrop = bdScript.next = Instantiate(backgrounds[newBdNumber], newPosition, Quaternion.identity);
                // Move it half its width to the right to line up
                SpriteRenderer srendNew = newBackdrop.GetComponent<SpriteRenderer>();
                newPosition.x += srendNew.bounds.extents.x;
                newBackdrop.transform.position = newPosition;
                // Now assign the number and neighborly links
                backdrop newBdScript = newBackdrop.GetComponent<backdrop>();
                newBdScript.bdNumber = newBdNumber;
                newBdScript.prev = other.gameObject;
                // give it a toy
                new_toy = Instantiate(toys[newBdNumber], newBackdrop.transform.position, Quaternion.identity);
                toy toyScript = new_toy.GetComponent<toy>();
                toyScript.backdrop = newBackdrop;

            }
            if (!bdScript.prev) {
                // find the left edge
                SpriteRenderer srendCurrent = other.GetComponent<SpriteRenderer>();
                Vector3 newPosition = other.transform.position;
                newPosition.x = srendCurrent.bounds.min.x;
                // Instantiate the next backdrop
                int newBdNumber = bdScript.bdNumber - 1;
                if (newBdNumber < 0) newBdNumber = backgrounds.Length - 1;
                GameObject newBackdrop = bdScript.prev = Instantiate(backgrounds[newBdNumber], newPosition, Quaternion.identity);
                // Move it half its width to the right to line up
                SpriteRenderer srendNew = newBackdrop.GetComponent<SpriteRenderer>();
                newPosition.x -= srendNew.bounds.extents.x;
                newBackdrop.transform.position = newPosition;
                // Now assign the number and neighborly links
                backdrop newBdScript = newBackdrop.GetComponent<backdrop>();
                newBdScript.bdNumber = newBdNumber;
                newBdScript.next = other.gameObject;
                // give it a toy
                new_toy = Instantiate(toys[newBdNumber], newBackdrop.transform.position, Quaternion.identity);
                toy toyScript = new_toy.GetComponent<toy>();
                toyScript.backdrop = newBackdrop;
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

    void SmoothFollowCamera() {
        // find a target if I can
        target = null;
        GameObject[] foundObjects;
        for (int i = 0; i < toys.Length; i++) {
            foundObjects = GameObject.FindGameObjectsWithTag("toy");
            foreach(GameObject foundObject in foundObjects) {
                if (foundObject.GetComponent<toy>().Interactive)
                    target = foundObject.transform;
            }
        }
        if (target) {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            destination.y = initialPosition.y; destination.z = initialPosition.z;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }


    }

    // code stolen from http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html
    //public class SmoothCamera2D : MonoBehaviour {

    //    public float dampTime = 0.15f;
    //    private Vector3 velocity = Vector3.zero;
    //    public Transform target;

    //    // Update is called once per frame
    //    void Update() {
    //        if (target) {
    //            Vector3 point = camera.WorldToViewportPoint(target.position);
    //            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
    //            Vector3 destination = transform.position + delta;
    //            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    //        }

    //    }
    //}

}
