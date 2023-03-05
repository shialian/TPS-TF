using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Slider bgm;
    public Slider sound;
    public GameObject startMenu;
    public GameObject pauseMenu;

    private void Start()
    {
        bgm.value = AudioManager.singleton.source.volume;
        sound.value = SoundManager.singleton.source.volume;
    }

    private void Update()
    {
        AudioManager.singleton.source.volume = bgm.value;
        SoundManager.singleton.source.volume = sound.value;
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
