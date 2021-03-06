using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIntroduction : MonoBehaviour
{
    public GameObject[] introductions;
    public GameObject[] weaponIntroductions;
    public GameObject[] bubbleIntroductions;
    public Text startText;

    public GameObject playerUI;
    public Player player;
    public PlayerLocomotion locomotion;
    public CameraFollow cam;

    private int index;
    private int initLength;
    private int totalLength;
    private float oriSpeedFactor;
    private GameObject currIntroduction;

    private void Start()
    {
        initLength = introductions.Length;
        totalLength = introductions.Length + weaponIntroductions.Length + bubbleIntroductions.Length;
        startText.text = "遊戲開始";
        currIntroduction = introductions[0];
        oriSpeedFactor = 5f;
    }

    public void Init(string from)
    {
        index = 0;
        currIntroduction = introductions[0];
        if(from == "start")
        {
            startText.text = "遊戲開始";
        }
        else
        {
            startText.text = "回到遊戲";
        }
    }

    public void Next()
    {
        index++;
        if (startText.text == "遊戲開始")
        {
            if (index >= initLength)
            {
                index -= initLength;
            }
            index %= initLength;
            SetInitIntro();
        }
        else
        {
            if (index >= totalLength)
            {
                index -= totalLength;
            }
            index %= totalLength;
            SetIntro();
        }
    }

    public void Prev()
    {
        index--;
        if (startText.text == "遊戲開始")
        {
            if (index < 0)
            {
                index += initLength;
            }
            index %= initLength;
            SetInitIntro();
        }
        else
        {
            if (index < 0)
            {
                index += totalLength;
            }
            index %= totalLength;
            SetIntro();
        }
    }

    private void SetIntro()
    {
        int i = index;
        currIntroduction.SetActive(false);
        if(i - introductions.Length >= 0)
        {
            i -= introductions.Length;
            if(i - weaponIntroductions.Length >= 0)
            {
                i -= weaponIntroductions.Length;
                currIntroduction = bubbleIntroductions[i];
            }
            else
            {
                currIntroduction = weaponIntroductions[i];
            }
        }
        else
        {
            currIntroduction = introductions[i];
        }
        currIntroduction.SetActive(true);
    }

    private void SetInitIntro()
    {
        currIntroduction.SetActive(false);
        currIntroduction = introductions[index];
        currIntroduction.SetActive(true);
    }

    public void StartGame()
    {
        if (startText.text == "遊戲開始")
        {
            startText.text = "回到遊戲";
            Cursor.lockState = CursorLockMode.Locked;
            playerUI.SetActive(true);
            locomotion.movingSpeedFactor = oriSpeedFactor;
            player.oriSpeedFactor = oriSpeedFactor;
            cam.enabled = true;
            Cursor.visible = false;
            AudioManager.singleton.SetGaming();
            gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.SetActive(false);
        }
    }
}
