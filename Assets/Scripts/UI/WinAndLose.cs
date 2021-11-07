using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAndLose : MonoBehaviour
{
    public void ReloadGame()
    {
        GameManager.singleton.ReloadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
