using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;          //for the benefit of other scripts
    public AudioSource musicSource;
    public AudioSource generalSoundSource;

    public bool engineIsOn;
    public bool turningOffEngine;
    public bool turningOnEnginge;


    // Use this for initialization
    void Awake()
    {
        //construct instance
        //safety net incase something weird happens
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyObject(gameObject);
        }
       // DontDestroyOnLoad(gameObject);

        PauseOrPlayMusic(false);
    }

    //Used to play sound clips.
    public void PlaySound(AudioSource soundSource, AudioClip sound, bool loop)
    {
        //Play the clip.
        soundSource.loop = loop;
        soundSource.PlayOneShot(sound);
    }

    public void StartOrStopEngine(AudioSource engineSource, bool start)
    {
        if (start)
        {
            engineIsOn = true;
            StartCoroutine(FadeIn(engineSource, 5f));                         
        }
        else
        {
            engineIsOn = false;
            StartCoroutine(FadeOut(engineSource, 2f));                                 
        }
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        //audioSource.Play();
        turningOnEnginge = true;
       
        float startVolume = 0.1f;

        while (audioSource.volume < 1 && engineIsOn)
        {
            audioSource.volume += startVolume * Time.deltaTime * FadeTime;

            yield return null;
        }
        turningOnEnginge = false;
        //audioSource.volume = startVolume;
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        turningOffEngine = true;
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0 && !engineIsOn)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        turningOffEngine = false;
        //audioSource.Stop();
        //audioSource.volume = startVolume;
    }

    public void PauseOrPlayMusic(bool pause)
    {
        if (pause)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }

    }
}
