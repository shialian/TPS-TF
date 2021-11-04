using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public RuntimeAnimatorController pistolAnimator;
    public RuntimeAnimatorController assultAnimator;

    private Animator anim;
    private Player player;
    private PlayerLocomotion locomotion;

    private void Start()
    {
        anim = GetComponent<Animator>();
        locomotion = GetComponent<PlayerLocomotion>();
        player = GetComponent<Player>();
        SetAttackType(player.holdingGun.gunType, player.holdingGun.attackType);
    }

    private void Update()
    {
        SetRunning();
        SetJump();
        SetAttack();
    }

    private void SetRunning()
    {
        anim.SetFloat("Forward Speed", locomotion.forwardSpeed);
        anim.SetFloat("Right Speed", locomotion.rightSpeed);
        anim.SetFloat("Speed", locomotion.movingSpeed);
    }

    private void SetJump()
    {
        anim.SetBool("IsJumping", locomotion.isJumping);
    }

    private void SetAttack()
    {
        if (Input.GetButton("FireOnlyMouse"))
        {
            anim.SetBool("IsAttack", true);
        }
        else
        {
            anim.SetBool("IsAttack", false);
        }
    }

    public void Falling()
    {
        anim.SetBool("IsFalling", true);
        Invoke("FallingEnd", 0.3f);
    }

    private void FallingEnd()
    {
        anim.SetBool("IsFalling", false);
    }

    public void SetAttackType(Gun.GunType gunType, Gun.AttackType attackType)
    {
        switch (gunType)
        {
            case Gun.GunType.Assault:
                anim.runtimeAnimatorController = assultAnimator;
                break;
            case Gun.GunType.Pistol:
                anim.runtimeAnimatorController = pistolAnimator;
                break;
        }
        anim.SetFloat("AttackType", (float)attackType);
    }
}
