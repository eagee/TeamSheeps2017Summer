using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Dot")
        {
            if(coll.gameObject.GetComponent<TinyDotScript>())
                coll.gameObject.GetComponent<TinyDotScript>().BringToLife();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
