using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Slider bgm;
    public Slider sound;
    public Slider fighterSound;
    public GameObject startMenu;
    public GameObject pauseMenu;
    public AudioSource fighterAudio;

    private void Start()
    {
        bgm.value = AudioManager.singleton.source.volume;
        sound.value = SoundManager.singleton.source.volume;
        fighterSound.value = fighterAudio.volume;
    }

    private void Update()
    {
        AudioManager.singleton.source.volume = bgm.value;
        SoundManager.singleton.source.volume = sound.value;
        fighterAudio.volume = fighterSound.value;
    }

    public void Prev()
    {
        if (GameManager.singleton.gameStarted)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            startMenu.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
