using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backdrop : MonoBehaviour {

    public GameObject prev = null, next = null;
    public int bdNumber = -1;
    public bool hasCamera = false;
    public bool destroyMyself = false;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (destroyMyself) {
            if (hasCamera) {
                Debug.Log("Wait! I, " + gameObject + " want to be destroyed but I am on camera!", gameObject);
                destroyMyself = false;
            } else if (prev && next) {
                Debug.Log("Wait! I, " + gameObject + " want to be destroyed but I have two neighbors!", gameObject);
            } else if (prev && prev.GetComponent<backdrop>().next) {
                Debug.Log("Wait! I, " + gameObject + " want to be destroyed but prev is pointing at me!", gameObject);
                prev.GetComponent<backdrop>().next = null;
            } else if (next && next.GetComponent<backdrop>().prev) {
                Debug.Log("Wait! I, " + gameObject + " want to be destroyed but next is pointing at me!", gameObject);
                next.GetComponent<backdrop>().prev = null;
            } else {
                Destroy(gameObject);
            }
        } else {
            // Consistency check!
            if (prev && prev.GetComponent<backdrop>()) {
                Debug.AssertFormat(prev.GetComponent<backdrop>().next == gameObject, gameObject, "prev.next {0} not equal to me, {1}", prev.GetComponent<backdrop>().next, gameObject);
                // Debug.Break();
            }
            if (next && next.GetComponent<backdrop>()) {
                Debug.AssertFormat(next.GetComponent<backdrop>().prev == gameObject, gameObject, "next.prev {0} not equal to me, {1}", next.GetComponent<backdrop>().prev, gameObject);
                // Debug.Break();
            }
            //Vector3 currPosition = transform.position;
            //if (hasCamera) {
            //    currPosition.y = Mathf.Sin(Time.time * 10f);
            //    transform.position = currPosition;
            //} else {
            //    currPosition.y = 0f;
            //    transform.position = currPosition;
            //}
        }
    }
}
