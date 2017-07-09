using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {
    public bool ConstantAnimation = false;
    public Sprite frameA;
    public Sprite frameB;
    private float timer = 0f;
    public float waitTime = 1f;
    private int m_frameID;
    private float m_frameTime = 0.015f;
    private float m_frameTimer = 0f;
	// Use this for initialization
	void Start () {
        timer = 0f;
        
	}
	
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        { 
            timer = waitTime;
            m_frameTime = 0.015f;
            m_frameTimer = 0f;
        }
    }
	// Update is called once per frame
	void Update () {

        if(ConstantAnimation == true && timer > 0f)
        {
            timer -= Time.deltaTime;
            m_frameTimer += Time.deltaTime;
            if(m_frameTimer > m_frameTime)
            {
                m_frameID++;
                if (m_frameID > 1) m_frameID = 0;
                m_frameTimer = 0f;
                m_frameTime += 0.015f;
                if(m_frameID == 0)
                {
                    GetComponent<SpriteRenderer>().sprite = frameB;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = frameA;
                }
            }
        }
        else
        {
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
}
