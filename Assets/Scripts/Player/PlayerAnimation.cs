using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public RuntimeAnimatorController pistolAnimator;
    public RuntimeAnimatorController assultAnimator;

    [HideInInspector]
    public Animator anim;
    private Player player;
    private PlayerLocomotion locomotion;

    private string idleAttackState = "Idle Attacking";
    private string runningAttackState = "Attack Running";
    private string jumpAttackState = "Jump Attacking";

    private void Start()
    {
        anim = GetComponent<Animator>();
        locomotion = GetComponent<PlayerLocomotion>();
        player = GetComponent<Player>();
        SetAttackType(player.holdingGun.gunType, player.holdingGun.attackType);
    }

    private void Update()
    {
        if (locomotion.movingSpeedFactor > 0f)
        {
            SetRunning();
            SetJump();
            SetAttack();
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }
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
        if (Input.GetButton("FireOnlyMouse") && player.holdingGun != null)
        {
            anim.SetBool("IsAttack", true);
        }
        else
        {
            anim.SetBool("IsAttack", false);
        }
    }

    public void SetState()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(idleAttackState) ||
            anim.GetCurrentAnimatorStateInfo(0).IsName(runningAttackState) ||
            anim.GetCurrentAnimatorStateInfo(0).IsName(jumpAttackState))
        {
            anim.SetBool("IsDead", false);
            player.state = Player.State.Attacking;
        }
        else
        {
            anim.SetBool("IsDead", false);
            player.state = Player.State.Normal;
        }
        if (player.isDead)
        {
            anim.SetBool("IsDead", true);
            player.state = Player.State.die;
        }
    }

    public void Falling()
    {
        anim.SetBool("IsFalling", true);
        Invoke("FallingEnd", 0.1f);
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
