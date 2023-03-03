using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager singleton;

    public RoundManager roundManager;
    public EnemyManager enemyManager;
    public Text roundsText;
    public GameObject remindText;
    public bool winTheGame = false;

    public GunManager gunManager;
    public Transform[] availableBlocks;

    public Fighter fighter;
    public bool bomb = true;
    public GameObject bombIcon;
    public GameObject fighterSupportIcon;

    public bool poisonous = false;
    public Material virus;

    public float colddown = 2f;
    private float timer = 20f;

    public bool playerIsDead = false;

    public bool gameStarted = false;
    public GameObject winMenu;
    public GameObject loseMenu;

    private void Start()
    {
        Time.timeScale = 1f;
        singleton = this;
        winTheGame = false;
        gameStarted = false;
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        Input.imeCompositionMode = IMECompositionMode.Off;
    }

    private void FixedUpdate()
    {
        if (enemyManager.allEnemyDead == false)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= colddown)
            {
                SetFighterSupport();
                timer = 0f;
            }
        }
        if(roundManager.roundIndex == 15 && enemyManager.q.Count == 0 && enemyManager.allEnemyDead)
        {
            winTheGame = true;
            Invoke("WinTheGame", 3f);
        }
    }

    private void SetFighterSupport()
    {
        int supportCount = 3;
        Waypoints waypoints = fighter.GetComponent<Waypoints>();
        waypoints.waypoints = new Transform[supportCount + 1];
        waypoints.waypoints[0] = fighter.transform;
        for(int i = 1; i <= supportCount; i++)
        {
            Transform selectblock = availableBlocks[Random.Range(0, availableBlocks.Length)];
            waypoints.waypoints[i] = selectblock;
        }
        
        Fighter f = Instantiate(fighter);
        f.Initialize(gunManager, waypoints, enemyManager);
        fighterSupportIcon.SetActive(true);
    }

    public void WinTheGame()
    {
        AudioManager.singleton.SetWin();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoseTheGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.singleton.SetLose();
        loseMenu.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && gameStarted && enemyManager.allEnemyDead && roundManager.roundIndex < 15)
        {
            roundManager.NewRoundStart();
            roundsText.text = roundManager.roundIndex.ToString();
            remindText.SetActive(false);
            bomb = true;
            bombIcon.SetActive(true);
            timer = colddown;
        }
    }

    public void SetRemindTextOn()
    {
        remindText.SetActive(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Velley");
    }
}
