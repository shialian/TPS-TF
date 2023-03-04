using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;

    [SerializeField]
    private GameObject remindText;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject winMenu;
    [SerializeField]
    private GameObject loseMenu;

    private void Start()
    {
        singleton = this;
        HideMenu();
    }

    private void HideMenu()
    {
        remindText.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.singleton.gameStarted)
        {
            Pause();
        }
    }

    private void Pause()
    {
        bool state = !pauseMenu.activeSelf;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
        Time.timeScale = state ? 0f : 1f;
        pauseMenu.SetActive(state);
        if (state)
        {
            SoundManager.singleton.Pause();
        }
        else
        {
            SoundManager.singleton.Resume();
        }
    }

    public void SetRemind(bool activation)
    {
        remindText.SetActive(activation);
    }

    public void Resume(GameObject ui)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        ui.SetActive(false);
        SoundManager.singleton.Resume();
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
