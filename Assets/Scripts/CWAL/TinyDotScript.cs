using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyDotScript : MonoBehaviour {

    public float leftSpawnPoint = -15f;
    public float rightSpawnPoint = 15f;
    
    public float minSize = 0.15f;
    public float maxSize = 0.30f;
    public float minLife = 5.0f;
    public float maxLife = 40.0f;

    private float lifeUsed = 0.0f;
    private float lifeAllowed = 40.0f;
    private float m_waterLevelOffset = 0f;
    private float m_bringToLifeCounter = 0f;
    private bool m_bringToLifeTriggered = false;
    private EventTimer m_eventTimer;


    void setupDot()
    {
        //Vector3 startingPostion = new Vector3(Random.Range(0.3f, 2.5f), Random.Range(7f, 8f), 0.0f);
        //works with cwal proto Vector3 startingPostion = new Vector3(Random.Range(2.15f, 2.2f), Random.Range(9f, 20f), 0.0f);
        //Vector3 startingPostion = new Vector3(Random.Range(leftSpawnPoint, rightSpawnPoint), Random.Range(9f, 30f), 0.0f);
        //this.transform.position = startingPostion;
        float size = Random.Range(minSize, maxSize);
        Vector3 newScale = new Vector3(size, size, 1.0f);
        gameObject.transform.localScale = newScale;
        gameObject.GetComponent<Rigidbody>().mass = size;
        lifeUsed = 0.0f;
        lifeAllowed = Random.Range(minLife, maxLife);
        m_bringToLifeCounter = 0.15f;
        m_bringToLifeTriggered = false;
        //Color currentColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        //currentColor.a = 0f;
        //this.gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }

    /// <summary>
    ///  Called by other objects on collision to bring this dot to life on contact.
    /// </summary>
    public void BringToLife()
    {
        if (this.transform.position.y < m_eventTimer.GetWaterLevel())
            m_bringToLifeTriggered = true;
    }

    // Use this for initialization
    void Start ()
    {
        m_waterLevelOffset = Random.Range(0.01f, 10f);
        m_eventTimer = FindObjectOfType<EventTimer>();
        setupDot();
    }

    private void FadeAlphaToTarget(float fadeSpeed, float targetAlpha)
    {
        Color currentColor = GetComponent<SpriteRenderer>().material.color;

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
        GetComponent<SpriteRenderer>().material.color = currentColor;
    }

    // Update is called once per frame
    void Update ()
    {
        if(m_bringToLifeTriggered == true)
        {
            m_bringToLifeCounter -= Time.deltaTime;
            if (m_bringToLifeCounter < 0f)
                m_waterLevelOffset = 0f;
        }

        if (this.transform.position.y + m_waterLevelOffset < m_eventTimer.GetWaterLevel())
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<SphereCollider>().enabled = true;
            lifeUsed += Time.deltaTime;
            if(lifeUsed > lifeAllowed - 2f)
            {
                FadeAlphaToTarget(1f, 0f);//lifeUsed / lifeAllowed
            }
            if (lifeUsed > lifeAllowed || this.transform.position.y <= -22.0f)
            {
                Destroy(this.gameObject);
            }
        }

        //Color currentColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        //if(currentColor.a < 1f)
        //{
        //    currentColor.a += Time.deltaTime / 2f;
        //}
        //this.gameObject.GetComponent<SpriteRenderer>().color = currentColor;

    }
}
