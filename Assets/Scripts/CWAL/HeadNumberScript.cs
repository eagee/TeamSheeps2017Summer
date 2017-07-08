using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadNumberScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
    {
        int bodyNumber = GetComponent<JointTracker>().BodyNumber;
        GetComponentInChildren<TextMesh>().text = bodyNumber.ToString();
	}
}
