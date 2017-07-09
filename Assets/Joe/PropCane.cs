using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCane : MonoBehaviour {

    public bool hasMoved = false;
    public bool stillNow = false;
    public float timeToStart;
    public float lastMoved;
    public Collider colly;
    public Vector3 initialPosition, previousPosition;
    public float stillTime = 1f;

	// Use this for initialization
	void Start () {
        colly = GetComponent<Collider>();
        initialPosition = previousPosition = transform.position;
        timeToStart = Time.time + 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (stillNow) return;
        if (Time.time < timeToStart) {
            // ignore any initial movement
            initialPosition = transform.position;
            return;
        }
        if (hasMoved) {
            // still moving?
            if (Vector3.Distance(transform.position, previousPosition) > 0.05f) {
                lastMoved = Time.time;
                previousPosition = transform.position;
            } else {
                if (Time.time > lastMoved + stillTime) {
                    // immune to further movement by wind
                    gameObject.tag = "Finish";
                    if (!FadeAlphaToTarget(0.5f, 0.5f)) stillNow = true;
                }
            }
        } else {
            if (Vector3.Distance(transform.position, initialPosition) > 0.1f) {
                hasMoved = true;
                lastMoved = Time.time;
            }
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
