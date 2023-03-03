using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton;

    // BGM
    public AudioClip opening;
    public AudioClip gaming;
    public AudioClip win;
    public AudioClip lose;

    public AudioSource source;

    private void Start()
    {
        singleton = this;
        source = GetComponent<AudioSource>();
        SetOpening();
    }

    public void SetOpening()
    {
        source.clip = opening;
        source.loop = true;
        source.Play();
    }

    public void SetGaming()
    {
        source.clip = gaming;
        source.loop = true;
        source.Play();
    }

    public void SetWin()
    {
        source.clip = win;
        source.loop = false;
        source.Play();
    }

    public void SetLose()
    {
        source.clip = lose;
        source.loop = false;
        source.Play();
    }

    
}
