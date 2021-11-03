using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerLocomotion locomotion;

    private void Start()
    {
        anim = GetComponent<Animator>();
        locomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", locomotion.movingSpeed);
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }
}
