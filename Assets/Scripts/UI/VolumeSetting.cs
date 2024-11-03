using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private Button prevButton;

    // public Slider bgm;
    // public Slider sound;

    private void Awake()
    {
        // bgm.value = AudioManager.singleton.source.volume;
        // sound.value = SoundManager.singleton.source.volume;
        prevButton.onClick.AddListener(Prev);
        EventManager.Subscribe(EventType.UI.OpenVolumeSetting, Show);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // AudioManager.singleton.source.volume = bgm.value;
        // SoundManager.singleton.source.volume = sound.value;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Prev()
    {
        gameObject.SetActive(false);
    }
}
