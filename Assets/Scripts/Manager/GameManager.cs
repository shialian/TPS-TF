using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager singleton;

    public RoundManager roundManager;
    public EnemyManager enemyManager;
    public Text roundsText;
    public GameObject remindText;

    public GunManager gunManager;
    public Transform[] availableBlocks;

    public Fighter fighter;
    public bool bomb = true;
    public GameObject bombIcon;

    public bool poisonous = false;
    public Material virus;

    public float colddown = 2f;
    private float timer = 20f;

    public bool playerIsDead = false;

    public GameObject pauseMenu;

    private void Start()
    {
        singleton = this;
        pauseMenu.SetActive(false);
        DontDestroyOnLoad(this);
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
        int supportCount = Random.Range(3, 6);
        Waypoints waypoints = fighter.GetComponent<Waypoints>();
        waypoints.waypoints = new Transform[supportCount + 1];
        waypoints.waypoints[0] = fighter.transform;
        for(int i = 1; i <= supportCount; i++)
        {
            Transform selectblock = availableBlocks[Random.Range(0, availableBlocks.Length)];
            waypoints.waypoints[i] = selectblock;
        }
        
        Fighter f = Instantiate(fighter);
        f.Initialize(gunManager, waypoints);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && enemyManager.allEnemyDead)
        {
            roundManager.NewRoundStart();
            roundsText.text = roundManager.roundIndex.ToString();
            remindText.SetActive(false);
            bomb = true;
            bombIcon.SetActive(true);
            timer = colddown;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void SetRemindTextOn()
    {
        remindText.SetActive(true);
    }
}
