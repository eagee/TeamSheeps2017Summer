using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

    public class AttachedObject
    {
        public GameObject TargetObject;
        public float ObjectTimer;
        public float MaxAttachTime;

        public AttachedObject(GameObject gameObject, float timerInit, float maxAttachTime)
        {
            TargetObject = gameObject;
            MaxAttachTime = maxAttachTime;
            ObjectTimer = timerInit;
        }
    }

    public float AttachForTime = 1f;
    private float m_Timer = 0f;
    private List<AttachedObject> m_AttachedObjects;

	// Use this for initialization
	void Start () {
        m_AttachedObjects = new List<AttachedObject>();
        m_AttachedObjects.Clear();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Interactive")
        {
            AttachedObject newObject = new AttachedObject(coll.gameObject, 0.0f, AttachForTime);
            m_AttachedObjects.Add(newObject);
        }
    }

    // Update is called once per frame, if m_AttachedObject isn't null, then we'll increment
    // the timer and change the behavior of the attached object and our trigger until
    // the timer is up.
    void Update ()
    {
        if(m_AttachedObjects.Count > 0)
        {
            foreach(var target in m_AttachedObjects)
            {
                target.ObjectTimer += Time.deltaTime;
                if (target.ObjectTimer > target.MaxAttachTime)
                {
                    m_AttachedObjects.Remove(target);
                    break;
                }
                else
                {
                    target.TargetObject.GetComponent<Rigidbody>().AddForce(this.transform.position - target.TargetObject.transform.position);
                    target.TargetObject.GetComponent<Rigidbody>().AddForce(this.transform.position - target.TargetObject.transform.position);
                    target.TargetObject.GetComponent<Rigidbody>().AddForce(this.transform.position - target.TargetObject.transform.position);
                    target.TargetObject.GetComponent<Rigidbody>().AddForce(this.transform.position - target.TargetObject.transform.position);
                }
            }
        }
    }
}
