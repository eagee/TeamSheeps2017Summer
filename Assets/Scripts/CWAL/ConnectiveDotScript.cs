using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectiveDotScript : MonoBehaviour
{
    public GameObject ParentJoint;
    public GameObject ChildJoint;

    public float minSize = 0.15f;
    public float maxSize = 0.30f;
    
    private float m_lerpBetweenJoints = 0f;

    public void SetKinematic(bool enabled)
    {
        GetComponent<Rigidbody2D>().isKinematic = enabled;
    }

    public void SetupDot()
    {
        // Randomly set the size of the dot based on the parameters above.
        float size = Random.Range(minSize, maxSize);
        Vector3 newScale = new Vector3(size, size, 1.0f);
        gameObject.transform.localScale = newScale;
        gameObject.GetComponent<Rigidbody2D>().mass = size;

        // Randomly define a lerp between the ParentJoint and Child Joint which this dot will always follow (placing it at some point between the two joints.
        m_lerpBetweenJoints = Random.Range(0.0f, 1.0f);
        
        //Color currentColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        //currentColor.a = 0f;
        //this.gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody2D>().isKinematic)
           this.transform.position = Vector3.Lerp(ParentJoint.transform.position, ChildJoint.transform.position, m_lerpBetweenJoints);
    }
}
