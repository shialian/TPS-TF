using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject playerUI;
    public Player player;
    public PlayerLocomotion locomotion;
    public CameraFollow cam;
    public GameObject intro;

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
        intro.SetActive(true);
        intro.GetComponent<GameIntroduction>().Init("Start");
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
