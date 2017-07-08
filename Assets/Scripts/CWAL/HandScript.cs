using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
    public float AttachTime = 1f;
    public float DisabledTime = 1f;
    public float LerpSpeed = 1f;
    private float m_Timer = 0f;
    private GameObject m_AttachedObject;

	// Use this for initialization
	void Start () {
        m_Timer = 0f;
        m_AttachedObject = null;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Interactive" && m_AttachedObject == null)
        {
            m_AttachedObject = coll.gameObject;
            m_Timer = 0f;
        }
    }

    // Update is called once per frame, if m_AttachedObject isn't null, then we'll increment
    // the timer and change the behavior of the attached object and our trigger until
    // the timer is up.
    void Update ()
    {
        if(m_AttachedObject != null)
        {
            m_Timer += Time.deltaTime;
            if(m_Timer < AttachTime)
            {
                Vector3 newPosition = Vector3.Lerp(m_AttachedObject.transform.position, this.transform.position, LerpSpeed);
                m_AttachedObject.transform.position = newPosition;
            }
            if(m_Timer > DisabledTime)
            {
                m_Timer = 0f;
                m_AttachedObject = null;
            }
        }
    }
}
