using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotGenerator : MonoBehaviour {

    public GameObject DotPrefab;
    public int Rate = 3;
    public float Delay = 0.1f;
    private float m_counter = 0.0f;

    // Use this for initialization
    void Start () {
        m_counter = 0.0f;
    }
    
    // Update is called once per frame
    void Update () {
        if(DotPrefab == null)
        {
            print("ERROR: DotPrefab not populated in inspector!!");
            return;
        }

        m_counter += Time.deltaTime;
        if(m_counter > Delay)
        {
            m_counter = 0;
            for(int x = 0; x < Rate; x++)
            {
                GameObject.Instantiate(DotPrefab, this.transform.position, Quaternion.identity);
            }
        }

    }
}
