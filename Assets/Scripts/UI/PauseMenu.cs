using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject intro;
    public GameObject setting;

    public void Resume()
    {
        UIManager.singleton.Resume(gameObject);
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
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
