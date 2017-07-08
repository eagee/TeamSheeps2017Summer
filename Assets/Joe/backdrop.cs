using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backdrop : MonoBehaviour {

    public GameObject prev = null, next = null;
    public int bdNumber = -1;
    public bool hasCamera = false;

	// Use this for initialization
	void Start () {
        Debug.Log("Starting backdrop Start()");
        //hasCamera = false;
        //prev = next = null;
        Debug.Log("Ending camera Start()");
    }

    // Update is called once per frame
    void Update () {
        Vector3 currPosition = transform.position;
        if (hasCamera) {
            currPosition.y = Mathf.Sin(Time.time * 10f);
            transform.position = currPosition;
        } else {
            currPosition.y = 0f;
            transform.position = currPosition;
        }
    }
}
