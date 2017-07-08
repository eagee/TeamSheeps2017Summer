﻿using System.Collections;
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

    [HideInInspector]
    public bool moveCamera;

    // Use this for initialization
    void Start () {
        GameObject new_bg;

        // Initial background at the same position as camera, but z of 0f.
        Vector3 bg_position = transform.position;
        bg_position.z = 0f;

        new_bg = Instantiate(backgrounds[initialBackdrop], bg_position, Quaternion.identity);
        backdrop bdScript = new_bg.GetComponent<backdrop>();
        if (bdScript) bdScript.bdNumber = initialBackdrop;
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
            // If I have a neighbor not on camera, destroy it.
            if (bdScript.prev && !bdScript.prev.GetComponent<backdrop>().hasCamera) {
                GameObject destroyMe = bdScript.prev;
                bdScript.prev = null;
                Destroy(destroyMe);
            }
            if (bdScript.next && !bdScript.next.GetComponent<backdrop>().hasCamera) {
                GameObject destroyMe = bdScript.next;
                bdScript.next = null;
                Destroy(destroyMe);
            }
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
