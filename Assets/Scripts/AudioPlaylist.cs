using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioPlaylist : MonoBehaviour
{
    public AudioClip loopClip;
    public GameObject tellingObject;
    public bool playWhenNull = false;
    private AudioSource audioSrc;

    //public AudioClip gameStartClip;
    //public AudioClip gameLoopClip;
    //private AudioSource audio;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;
        audioSrc.clip = loopClip;
        audioSrc.Play();
        //StartCoroutine(playGameSound());
    }

    void Update()
    {
        float modifier = 4f;
        if(playWhenNull && tellingObject == null)
        {
            if (audioSrc.volume < 1.0f) audioSrc.volume += Time.deltaTime / modifier;
        }
        else if(!playWhenNull && tellingObject == null)
        {
            if (audioSrc.volume > 0.0f) audioSrc.volume -= Time.deltaTime / modifier;
        }
        else if (playWhenNull && tellingObject != null)
        {
            if (audioSrc.volume > 0.0f) audioSrc.volume -= Time.deltaTime / modifier;

        }
        else if (!playWhenNull && tellingObject != null)
        {
            if (audioSrc.volume < 1.0f) audioSrc.volume += Time.deltaTime / modifier;
        }
    }
    
    //IEnumerator playGameSound()
    //{
    //    if(playWhenNull)
    //    {
    //        
    //    }
    //
    //    if(tellingObject != null && !playWhenNull)
    //    {
    //        //AudioPlaylist.clip 
    //    }
    //    //audio.clip = gameStartClip;
    //    //audio.Play();
    //    //yield return new WaitForSeconds(audio.clip.length);
    //    //audio.clip = gameLoopClip;
    //    //audio.Play();
    //}

    //void Start()
    //{
    //    audio = GetComponent<AudioSource>();
    //    audio.loop = true;
    //    StartCoroutine(playGameSound());
    //}
    //
    //IEnumerator playGameSound()
    //{
    //
    //    audio.clip = gameStartClip;
    //    audio.Play();
    //    yield return new WaitForSeconds(audio.clip.length);
    //    audio.clip = gameLoopClip;
    //    audio.Play();
    //}
}