using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject intro;
    public GameObject setting;

    public void Continue()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void GameIntro()
    {
        intro.SetActive(true);
        intro.GetComponent<GameIntroduction>().Init("Pause");
        gameObject.SetActive(false);
    }

    public void VolumeSetting()
    {
        setting.gameObject.SetActive(true);
        GameManager.singleton.gameStarted = true;
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
