using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    public int initialBackdrop;
    public GameObject[] backgrounds;
    public GameObject[] toys;

    [HideInInspector]
    public bool moveCamera;

    // Smooth camera stuff
    public float dampTime = 0.15f;
    public Transform target;
    public Vector3 initialPosition;

    // Use this for initialization
    void Start() {
        initialPosition = transform.position;

        GameObject new_bg, new_toy;

        // Initial background at the same position as camera, but z of 0f.
        Vector3 bg_position = transform.position;
        bg_position.z = 0f;
        Vector3 toy_position = transform.position;
        toy_position.z = 0f;

        new_bg = Instantiate(backgrounds[initialBackdrop], bg_position, Quaternion.identity);
        backdrop bdScript = new_bg.GetComponent<backdrop>();
        if (bdScript) bdScript.bdNumber = initialBackdrop;
        new_toy = Instantiate(toys[initialBackdrop], bdScript.GetSpawnPoint(), Quaternion.identity);
        Toy toyScript = new_toy.GetComponent<Toy>();
        toyScript.backdrop = new_bg;
    }

    void Update() {
        FindTarget();
    }

    void FindTarget() {
        // find a target if I can
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("toy");
        foreach (GameObject foundObject in foundObjects) {
            if (foundObject.GetComponent<Toy>().Interactive)
                GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = foundObject.transform;
        }

    }

    private void OnTriggerExit(Collider other) {
        backdrop bdScript = other.GetComponent<backdrop>();
        if (bdScript) {
            bdScript.hasCamera = false;
            // TODO: Find a safe way to destroy off-camera backdrops.
        }
    }

    // Note that we will trigger enter on a newly instantiated background before it has completed its "Start".
    private void OnTriggerEnter(Collider other) {
        backdrop bdScript = other.GetComponent<backdrop>();
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
                new_toy = Instantiate(toys[newBdNumber], newBdScript.GetSpawnPoint(), Quaternion.identity);
                Toy toyScript = new_toy.GetComponent<Toy>();
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
                new_toy = Instantiate(toys[newBdNumber], newBdScript.GetSpawnPoint(), Quaternion.identity);
                Toy toyScript = new_toy.GetComponent<Toy>();
                toyScript.backdrop = newBackdrop;
            }
        }
    }

}
