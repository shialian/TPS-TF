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
            Transform selectblock = availableBlocks[Random.Range(0, availableBlocks.Length)];
            Vector3 instantPosition = selectblock.position + new Vector3(0f, 2f, 0f);
            gunManager.CreateGun(instantPosition);
            timer = 0f;
        }
    }
}
