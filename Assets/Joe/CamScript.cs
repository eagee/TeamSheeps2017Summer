using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    private float speed = 1.0f;
    private float acceleration = 0.2f;
    private float maxSpeed = 3.2f;

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

        for (int i = 0; i < backgrounds.Length; i++) {
            new_bg = Instantiate(backgrounds[i], bg_position, Quaternion.identity);
            srend = new_bg.GetComponent<SpriteRenderer>();
            bg_position.x += srend.bounds.extents.x;
            new_bg.transform.position = bg_position;
            bg_position.x = srend.bounds.max.x;
        }

        moveCamera = true;
        Debug.Log("Ending camera Start()");
    }

    void Update() {
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
        }
    }

    void MoveCamera() {

        Vector3 temp = transform.position;

        temp.x = temp.x + (speed * Time.deltaTime);

        transform.position = temp;

        speed += acceleration * Time.deltaTime;

        if (speed > maxSpeed)
            speed = maxSpeed;

    }
}
