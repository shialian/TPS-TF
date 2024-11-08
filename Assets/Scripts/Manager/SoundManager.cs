﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;

    // SE
    public AudioClip shoot;
    public AudioClip bubble;
    public AudioClip bomb;
    public AudioClip die;
    public AudioClip airplane;

    public AudioSource source;

    private void Start()
    {
        singleton = this;
        source = GetComponent<AudioSource>();
    }

    public void SetShoot()
    {
        source.PlayOneShot(shoot);
    }

    public void SetBubble()
    {
        source.PlayOneShot(bubble);
    }

    public void SetBomb()
    {
        source.PlayOneShot(bomb);
    }

    public void SetDie()
    {
        source.PlayOneShot(die);
    }

    public void SetAirPlane()
    {
        source.PlayOneShot(airplane);
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Resume()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }
}
