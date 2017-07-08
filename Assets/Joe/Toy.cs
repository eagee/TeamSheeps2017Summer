using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour {

    public GameObject backdrop = null;
    public backdrop bdScript = null;

    Vector3 initialPosition;
    public bool Interactive = false;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (backdrop) { // don't do anything unless I have a backdrop
            if (!bdScript) bdScript = backdrop.GetComponent<backdrop>();
            if (bdScript.hasCamera) {
                Interactive = true;
            } else {
                Interactive = false;
            }
            //if (Interactive) {
            //    Vector3 newPosition = initialPosition;
            //    newPosition.x = initialPosition.x + 5f * Mathf.Sin(Time.time);
            //    newPosition.y = initialPosition.y + 2f * Mathf.Sin(Time.time * 1.2f);
            //    transform.position = newPosition;
            //}
        }
	}
}
