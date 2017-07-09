using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAfterTime : MonoBehaviour {

    public float hangTime = 1f;
    private float timeToHang;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        timeToHang = hangTime;	
	}
	
	// Update is called once per frame
	void Update () {
        timeToHang -= Time.deltaTime;
        if (timeToHang < 0f) {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        if (timeToHang < hangTime * -5f) {
            GameObject.Destroy(gameObject);
        }	
	}
}
