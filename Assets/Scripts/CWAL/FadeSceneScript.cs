using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneScript : MonoBehaviour {
    public float FadeSpeed = 2f;

    private bool m_FadeIn = false;
    private bool m_Active = true;

    private float m_FadeOutTimer = 0.0f;

    void Start()
    {
        m_FadeIn = true;
        m_Active = true;
        m_FadeOutTimer = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if(m_Active)
        {
            if(m_FadeIn)
                FadeAlphaToTarget(FadeSpeed, 0f);
            else
                FadeAlphaToTarget(FadeSpeed, 1f);
        }
        CheckForFadeOut();
    }

    void CheckForFadeOut()
    {
        TinyDotScript[] dot = FindObjectsOfType<TinyDotScript>();
        if (dot.Length < 20)
        {
            m_FadeOutTimer += Time.deltaTime;
            if (m_FadeOutTimer > 1f)
            {
                m_FadeIn = false;
                m_Active = true;
            }
        }
        
    }

    private void FadeAlphaToTarget(float fadeSpeed, float targetAlpha)
    {
        Color currentColor = new Color();
        currentColor = GetComponent<Renderer>().material.color;
        
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
        else
        {
            if (m_FadeIn)
                m_Active = false;
            else
                SceneManager.LoadScene("KinectPrototype");
        }


        GetComponent<Renderer>().material.color = currentColor;
    }
    
}
