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
            //FadeAlphaToTarget(1.0f, 0.0f);
        }
        if (timeToHang < hangTime * -5f) {
            GameObject.Destroy(gameObject);
        }	
	}

    // returns true if it can fade, false if already fully faded.
    private bool FadeAlphaToTarget(float fadeSpeed, float targetAlpha)
    {
        Color currentColor = GetComponent<SpriteRenderer>().material.color;

        if (currentColor.a < targetAlpha)
        {
            currentColor.a += fadeSpeed * Time.deltaTime;
            if (currentColor.a > targetAlpha) currentColor.a = targetAlpha;
        }
        else if (currentColor.a > targetAlpha)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            if (currentColor.a < targetAlpha) currentColor.a = targetAlpha;
        }
        else
        {
            return false;
        }
        GetComponent<SpriteRenderer>().material.color = currentColor;
        return true;
    }
}
