using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject playerUI;
    public PlayerLocomotion locomotion;
    public CameraFollow cam;

    private float oriSpeedFactor;

    private void Start()
    {
        playerUI.SetActive(false);
        oriSpeedFactor = locomotion.movingSpeedFactor;
        locomotion.movingSpeedFactor = 0f;
        cam.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        playerUI.SetActive(true);
        gameObject.SetActive(false);
        locomotion.movingSpeedFactor = oriSpeedFactor;
        cam.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GameIntro()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
