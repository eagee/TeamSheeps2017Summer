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
                FadeAlphaToTarget(10f, 1f);
            } else {
                Interactive = false;
                if (transform.position == initialPosition) {
                    FadeAlphaToTarget(10f, 1f);
                } else {
                    if (!FadeAlphaToTarget(1f, 0f)) {
                        transform.position = initialPosition;
                    }
                }
            }
            //if (Interactive) {
            //    Vector3 newPosition = initialPosition;
            //    newPosition.x = initialPosition.x + 5f * Mathf.Sin(Time.time);
            //    newPosition.y = initialPosition.y + 2f * Mathf.Sin(Time.time * 1.2f);
            //    transform.position = newPosition;
            //}
        }
	}

    // returns true if it can fade, false if already fully faded.
    private bool FadeAlphaToTarget(float fadeSpeed, float targetAlpha) {
        Color currentColor = GetComponent<SpriteRenderer>().material.color;

        if (currentColor.a < targetAlpha) {
            currentColor.a += fadeSpeed * Time.deltaTime;
            if (currentColor.a > targetAlpha) currentColor.a = targetAlpha;
        } else if (currentColor.a > targetAlpha) {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            if (currentColor.a < targetAlpha) currentColor.a = targetAlpha;
        } else {
            return false;
        }
        GetComponent<SpriteRenderer>().material.color = currentColor;
        return true;
    }

}
