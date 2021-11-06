using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager singleton;
    public float colddown = 2f;
    public GunManager gunManager;
    public Transform[] availableBlocks;

    public Fighter fighter;

    public bool poisonous = false;
    public Material virus;

    private float timer = 200f;

    private void Start()
    {
        singleton = this;
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= colddown)
        {
            SetFighterSupport();
            timer = 0f;
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
}
