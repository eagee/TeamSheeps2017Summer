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

        currBackground = 0;
        background = Instantiate(backgrounds[currBackground]);
        Debug.Log("Background scale: " + background.transform.localScale);
        currBackground += 1;
        SpriteRenderer srend = background.GetComponent<SpriteRenderer>();
        Vector3 newPosition = new Vector3(srend.bounds.max.x, background.transform.position.y, background.transform.position.z);
        background = Instantiate(backgrounds[currBackground], newPosition, Quaternion.identity);
        // now push it out by half its width
        srend = background.GetComponent<SpriteRenderer>();
        newPosition = new Vector3(background.transform.position.x + srend.bounds.extents.x, background.transform.position.y, background.transform.position.z);
        background.transform.position = newPosition;

        currBackground += 1;
        srend = background.GetComponent<SpriteRenderer>();
        newPosition = new Vector3(srend.bounds.max.x, background.transform.position.y, background.transform.position.z);
        background = Instantiate(backgrounds[currBackground], newPosition, Quaternion.identity);
        // now push it out by half its width
        srend = background.GetComponent<SpriteRenderer>();
        newPosition = new Vector3(background.transform.position.x + srend.bounds.extents.x, background.transform.position.y, background.transform.position.z);
        background.transform.position = newPosition;

        moveCamera = true;
	}

    void Update() {
        if (moveCamera) {
            MoveCamera();
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit " + other, gameObject);
        if (other.GetInstanceID() == background.GetInstanceID())
            other.gameObject.SetActive(false);
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
