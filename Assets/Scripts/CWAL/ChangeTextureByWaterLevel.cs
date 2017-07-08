using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureByWaterLevel : MonoBehaviour {

    public EventTimer eventTimer;
    public string ParameterToChange = "_BottomLimit";

    private float m_sineOffset;

    // Use this for initialization
    void Start () {
        m_sineOffset = 0.0f;
        if (eventTimer == null)
            eventTimer = FindObjectOfType<EventTimer>();
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.SetFloat(ParameterToChange, eventTimer.GetWaterLevel());
        GetComponent<Renderer>().material.SetFloat("_SinOffset", m_sineOffset);
        m_sineOffset += 0.1f;
    }
}

