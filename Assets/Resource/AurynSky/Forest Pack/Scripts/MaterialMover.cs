using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMover : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if (GameManager.singleton.poisonous)
        {
            rend.material.color = GameManager.singleton.virus.color;
        }
        else
        {
            rend.material.color = Color.white;
        }
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}