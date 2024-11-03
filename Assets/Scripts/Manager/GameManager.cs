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

    public Fighter fighter;
    public bool bomb = true;
    public GameObject bombIcon;

    public bool poisonous = false;
    public Material virus;

    public float colddown = 2f;
    private float timer = 20f;

    public bool playerIsDead = false;

    public bool gameStarted = false;

    private void Start()
    {
        singleton = this;
        gameStarted = false;
        winTheGame = false;
        Input.imeCompositionMode = IMECompositionMode.Off;
        Time.timeScale = 0f;
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
        
    }

    private void SetFighterSupport()
    {
        fighter.gameObject.SetActive(true);
        fighter.Initialize(gunManager, enemyManager);
        SoundManager.singleton.SetAirPlane();
    }

    public IEnumerator WinTheGame(float winAnimateTime)
    {
        gameStarted = false;
        winTheGame = true;
        AudioManager.singleton.SetWin();
        PlayerLocomotion.singleton.movingSpeedFactor = 0f;
        yield return new WaitForSeconds(winAnimateTime);
        UIManager.singleton.Win();
    }

    public void LoseTheGame()
    {
        gameStarted = false;
        AudioManager.singleton.SetLose();
        UIManager.singleton.Lose();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && gameStarted && enemyManager.allEnemyDead && roundManager.NoRounds() == false)
        {
            roundManager.NewRoundStart();
            roundsText.text = roundManager.roundIndex.ToString();
            UIManager.singleton.SetRemind(false);
            bomb = true;
            bombIcon.SetActive(true);
            timer = colddown;
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Velley");
    }
}
