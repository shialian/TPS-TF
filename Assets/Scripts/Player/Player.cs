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

    private float currentHP;

    private PlayerAnimation anim;

    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<PlayerAnimation>();
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
