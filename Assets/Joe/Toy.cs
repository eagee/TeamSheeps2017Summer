using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour {

    public GameObject backdrop = null;
    public backdrop bdScript = null;

    Vector3 initialPosition;
    public bool Interactive = false;
    public bool wasJustInteractive = false;
    public Vector3 lastInteractivePoint;
    public Collider colly;

	// Use this for initialization
	void Start () {
        colly = GetComponent<Collider>();
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (backdrop) { // don't do anything unless I have a backdrop
            if (!bdScript) bdScript = backdrop.GetComponent<backdrop>();
            if (bdScript.hasCamera) {
                Interactive = true;
                wasJustInteractive = true;
                lastInteractivePoint = transform.position;
                colly.enabled = true;
                FadeAlphaToTarget(10f, 1f);
            } else {
                Interactive = false;
                if (wasJustInteractive) {
                    Vector3 newToyLoc = newToyLocation();
                    float dist = Vector3.Distance(lastInteractivePoint, newToyLoc);
                    Debug.Log("Distance to travel: " + dist, gameObject);
                    colly.enabled = false;
                    if (!FadeAlphaAndPositionToTarget(10f / dist, 0f, lastInteractivePoint, newToyLoc)) {
                        transform.position = initialPosition;
                        wasJustInteractive = false;
                        colly.enabled = true;
                    }
                } else {
                    // FadeAlphaToTarget(10f, 0.5f);
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

    public Vector3 newToyLocation() {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("toy");
        foreach (GameObject foundObject in foundObjects) {
            if (foundObject.GetComponent<Toy>().Interactive)
                return foundObject.transform.position;
        }
        return Vector3.zero;
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

    // returns true if it can fade, false if already fully faded.
    private bool FadeAlphaAndPositionToTarget(float fadeSpeed, float targetAlpha, Vector3 origPosition, Vector3 targetPosition) {
        Color currentColor = GetComponent<SpriteRenderer>().material.color;
        int direction = 0;
        if (currentColor.a < targetAlpha) {
            currentColor.a += fadeSpeed * Time.deltaTime;
            direction = 1;
            if (currentColor.a > targetAlpha) currentColor.a = targetAlpha;
        } else if (currentColor.a > targetAlpha) {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            direction = -1;
            if (currentColor.a < targetAlpha) currentColor.a = targetAlpha;
        } else {
            return false;
        }
        GetComponent<SpriteRenderer>().material.color = currentColor;
        if (direction == 1) {
            Debug.Log("1 " + currentColor.a, gameObject);
            transform.position = Vector3.Lerp(origPosition, targetPosition, currentColor.a);
        } else if (direction == -1) {
            Debug.Log("2 " + currentColor.a, gameObject);
            transform.position = Vector3.Lerp(origPosition, targetPosition, (1f - currentColor.a));
        }

        return true;
    }

}
