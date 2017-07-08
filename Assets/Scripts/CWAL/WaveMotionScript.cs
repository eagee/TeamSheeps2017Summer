using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMotionScript : MonoBehaviour {
    public float LeftMost = -21.11f;
    public float RightMost = 33.39f;
    public float Speed = 1f;
    private float counter = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos.x -= Speed * Time.deltaTime;
        if (pos.x < LeftMost)
            pos.x = RightMost;
        if (pos.x > RightMost)
            pos.x = LeftMost;
        this.transform.position = pos;

        counter += Time.deltaTime;
        if(counter > 5f)
        {
            counter = 0.0f;
            //Speed = -Speed;
        }

    }
}
