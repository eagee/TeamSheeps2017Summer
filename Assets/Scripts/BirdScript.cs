using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {
    public Sprite frameA;
    public Sprite frameB;
    private float timer = 0f;
    public float waitTime = 1f;
	// Use this for initialization
	void Start () {
        timer = 0f;
        
	}
	
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
            timer = waitTime;
    }
	// Update is called once per frame
	void Update () {
		if (timer > 0f)
        {
            GetComponent<SpriteRenderer>().sprite = frameB;
            timer -= Time.deltaTime;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = frameA;
        }
	}
}
