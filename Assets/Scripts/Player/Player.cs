using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Gun holdingGun;

    public float maxHP = 100f;
    public Slider HPBar;

    private float currentHP;

    private void Start()
    {
        currentHP = maxHP;
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
        gun.parent = this.transform;
        gun.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
        gun.localRotation = Quaternion.identity;
        holdingGun = gun.GetComponent<Gun>();
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
}
