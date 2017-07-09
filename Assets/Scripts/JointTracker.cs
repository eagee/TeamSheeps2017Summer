using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class JointTracker : MonoBehaviour
{

    public JointType JointToUse;
    public BodySourceManager _bodyManager;
    public float scale = 8f;
    public float yOffset = -5f;
    public float xOffset = 0;
    public int BodyNumber = 0;
    private Camera m_Camera;

    private KinectJointFilter m_jointFilter;

    void Awake()
    {
        m_jointFilter = new KinectJointFilter();
        m_jointFilter.Init(0.55f, 0.25f, 2.0f, 0.30f, 1.25f);
        m_Camera = FindObjectOfType<Camera>();
    }

    // Get body data from the body manager and track the joint for the active body
    void Update()
    {
        if (_bodyManager == null)
        {
            return;
        }

        Body[] data = _bodyManager.GetData();
        if (data == null)
        {
            return;
        }

        // Use to test a single body when making changes!
        //foreach (Body body in data)
        //{
        //    if ((body != null) && (body.IsTracked))
        //    {
        //        GetComponent<Rigidbody>().isKinematic = true;
        //        GetComponent<SpriteRenderer>().enabled = true;
        //        GetComponentInChildren<SpriteRenderer>().enabled = true;
        //        //GetComponentInChildren<SpriteRenderer>().enabled = true;
        //
        //        //var pos = body.Joints[JointToUse].Position;
        //        //float yPosition = (pos.Y * scale) + yOffset;
        //        //Vector3 targetPosition = new Vector3(pos.X * scale, yOffset + pos.Y * scale, 0f);
        //        //this.transform.position = targetPosition;
        //
        //
        //        // Grab the mid spine position, we'll use this to make all other joint movement relative to the spine (this way we can limit the Y position of the character)
        //        var midSpinePosition = body.Joints[JointType.SpineMid].Position;
        //        var jointPos = body.Joints[JointToUse].Position;
        //        jointPos.X -= midSpinePosition.X;
        //        jointPos.Y -= midSpinePosition.Y;
        //        jointPos.Z -= midSpinePosition.Z;
        //        
        //        Vector3 targetPosition = new Vector3((midSpinePosition.X + jointPos.X) * scale, (yOffset + jointPos.Y) * scale, 0f);
        //        this.transform.position = targetPosition;
        //
        //        //float yPosition = (pos.Y * scale) + (yOffset + m_trackerDot.transform.position.y);
        //        //this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * 100);
        //        // Try moving to target vector with physics
        //        // Vector3 dir = (targetPosition - transform.position).normalized * 5f;
        //        // this.GetComponent<Rigidbody>().velocity = dir;
        //    }
        //    //else
        //    //{
        //    //    GetComponent<Rigidbody>().isKinematic = false;
        //    //    GetComponent<SpriteRenderer>().enabled = false;
        //    //    //GetComponentInChildren<SpriteRenderer>().enabled = false;
        //    //    var pos = this.transform.position;
        //    //    if (pos.y <= -12f)
        //    //    {
        //    //        pos.y = -12f;
        //    //        this.transform.position = pos;
        //    //    }
        //    //}
        //}


        // Use for actual multi-player environments!
        if ((data.Length >= BodyNumber) && (data[BodyNumber] != null) && (data[BodyNumber].IsTracked))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            //GetComponent<SpriteRenderer>().enabled = true;
            GetComponentInChildren<TrailRenderer>().enabled = true;
            FadeAlphaToTarget(2f, 1.0f);
            //GetComponentInChildren<SpriteRenderer>().enabled = true;

            m_jointFilter.UpdateFilter(data[BodyNumber]);
            var Joints = m_jointFilter.GetFilteredJoints();

            // Grab the mid spine position, we'll use this to make all other joint movement relative to the spine (this way we can limit the Y position of the character)
            var midSpinePosition = Joints[(int)JointType.SpineMid];
            var jointPos = Joints[(int)JointToUse];
            jointPos.X -= midSpinePosition.X;
            jointPos.Y -= midSpinePosition.Y;
            jointPos.Z -= midSpinePosition.Z;

            Vector3 targetPosition = new Vector3((xOffset + jointPos.X) * scale, (yOffset + jointPos.Y) * scale, 0f);
            targetPosition.x += m_Camera.transform.position.x;
            targetPosition.y += m_Camera.transform.position.y;

            this.transform.position = targetPosition;
        
            //float yPosition = (pos.Y * scale) + (yOffset + m_trackerDot.transform.position.y);
            //this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * 100);
            // Try moving to target vector with physics
            // Vector3 dir = (targetPosition - transform.position).normalized * 5f;
            // this.GetComponent<Rigidbody>().velocity = dir;
        }
        else
        {
            GetComponentInChildren<TrailRenderer>().enabled = false;
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<SpriteRenderer>().enabled = false;
            FadeAlphaToTarget(2f, 0.0f);
            //GetComponentInChildren<SpriteRenderer>().enabled = false;
            var pos = this.transform.position;
            if (pos.y <= -10f)
            {
                pos.x = -10;
                pos.y = -10f;
                this.transform.position = pos;
            }
        }

    }

    private void FadeAlphaToTarget(float fadeSpeed, float targetAlpha)
    {
        Color currentColor = new Color();
        currentColor = GetComponent<SpriteRenderer>().color;

        if (currentColor.a < targetAlpha)
        {
            currentColor.a += fadeSpeed * Time.deltaTime;
            if (currentColor.a > targetAlpha) currentColor.a = targetAlpha;
        }
        else if (currentColor.a > targetAlpha)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            if (currentColor.a < targetAlpha) currentColor.a = targetAlpha;
        }

        GetComponent<SpriteRenderer>().color = currentColor;
    }


}
