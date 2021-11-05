using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Gun holdingGun;
    public GunPack gunPack;

    public Vector3 playerMiddleFromPosition;

    public float maxHP = 100f;
    public Slider HPBar;
    public float recoverHP = 3f;
    private float currentHP;

    public float colddown = 1f;
    private float timer = 0f;

    private PlayerAnimation anim;
    public enum State
    {
        Normal,
        Attacking
    };
    [HideInInspector]
    public State state;

    private void Awake()
    {
        state = State.Normal;
    }

    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        anim.SetState();

        timer += Time.deltaTime;
        if(timer >= colddown)
        {
            currentHP += recoverHP;
            currentHP = Mathf.Min(currentHP, maxHP);
            HPBar.value = currentHP / maxHP;
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Gun":
                GunAttached(other.transform);
                break;
        }
    }

    private void GunAttached(Transform gun)
    {
        for(int i = 0; i < gunPack.guns.Length; i++)
        {
            Debug.Log(gunPack.guns[i].name + " " + gun.name);
            if(gunPack.guns[i].name == gun.name)
            {
                holdingGun.gameObject.SetActive(false);
                gunPack.guns[i].gameObject.SetActive(true);
                holdingGun = gunPack.guns[i];
                SetAttackType();
                gun.gameObject.SetActive(false);
            }
        }
    }

    private void SetAttackType()
    {
        anim.SetAttackType(holdingGun.gunType, holdingGun.attackType);
    }

    public void Damaged(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0f);
        HPBar.value = currentHP / maxHP;
        if(currentHP / maxHP <= 0.3f)
        {
            HPBar.GetComponentInChildren<Image>().color = Color.red;
        }
    }

    public Vector3 GetRayCastingPosition()
    {
        return transform.position + playerMiddleFromPosition;
    }
}
