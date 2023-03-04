using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Gun holdingGun;
    public GunPack gunPack;
    public GameObject[] gunIcon;
    public Text bulletAmountText;
    private GameObject currentGunIcon;

    public Vector3 playerMiddleFromPosition;

    public float maxHP = 100f;
    public Slider HPBar;
    public Image fill;
    public float recoverHP = 3f;
    public Sprite GreenHealth;
    public Sprite RedHealth;
    [SerializeField]
    private float currentHP;

    public bool isDead;
    public GameObject deadUI;

    public Image[] states;
    public Sprite[] stateImages;
    public Text[] countDowns;
    public float fireDamage = 3f;
    public float poiDamage = 2f;
    private bool onFire = false;
    private bool onEle = false;
    private bool onPoi = false;
    private int onFireIndex = -1;
    private int onEleIndex = -1;
    private int onPoiIndex = -1;
    private int stateIndex = 0;

    public float colddown = 1f;
    [SerializeField]
    private float timer = 0f;
    private float fireCountdown = 0f;
    private float eleCountdown = 0f;
    private float poiCountdown = 0f;
    private float oriColddown;

    private PlayerAnimation anim;

    public float oriSpeedFactor;
    private PlayerLocomotion locomotion;

    public enum State
    {
        Normal,
        Attacking,
        die
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
        locomotion = GetComponent<PlayerLocomotion>();
        currentGunIcon = gunIcon[0];
        oriColddown = colddown;
        deadUI.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(currentHP <= 0)
        {
            ForbiddenAnyActions();
        }
        anim.SetState();
        timer += Time.fixedDeltaTime;
        if (timer >= colddown)
        {
            currentHP += recoverHP;
            currentHP = Mathf.Min(currentHP, maxHP);
            HPBar.value = currentHP / maxHP;
            timer = 0f;
            if (currentHP / maxHP <= 0.3f)
            {
                fill.sprite = RedHealth;
            }
            else
            {
                fill.sprite = GreenHealth;
            }
            if (isDead && HPBar.value >= 0.3f)
            {
                AllowAnyActions();
            }
        }
        SetHealthByState();
    }

    private void ForbiddenAnyActions()
    {
        locomotion.movingSpeedFactor = 0f;
        if (onFire)
        {
            UpdateState(onFireIndex);
            fireCountdown = 0f;
            onFireIndex = -1;
            onFire = false;
        }
        if (onEle)
        {
            UpdateState(onEleIndex);
            eleCountdown = 0f;
            onEleIndex = -1;
            onEle = false;
        }
        if (onPoi)
        {
            UpdateState(onPoiIndex);
            poiCountdown = 0f;
            onPoiIndex = -1;
            onPoi = false;
        }
        colddown = 0.5f;
        deadUI.SetActive(true);
        isDead = true;
        SoundManager.singleton.SetDie();
        GameManager.singleton.playerIsDead = true;
    }

    private void AllowAnyActions()
    {
        locomotion.movingSpeedFactor = oriSpeedFactor;
        Debug.Log(locomotion.movingSpeedFactor);
        GameManager.singleton.playerIsDead = false;
        deadUI.SetActive(false);
        isDead = false;
        colddown = oriColddown;
        anim.SetState();
    }

    private void SetHealthByState()
    {
        if (onFire)
        {
            int retCountdown = (int)fireCountdown;
            fireCountdown -= Time.fixedDeltaTime;
            if((int)fireCountdown < retCountdown)
            {
                currentHP -= fireDamage;
                currentHP = Mathf.Min(currentHP, maxHP);
                HPBar.value = currentHP / maxHP;
            }
            if (fireCountdown <= 0f)
            {
                fireCountdown = 0f;
                UpdateState(onFireIndex);
                onFireIndex = -1;
                onFire = false;
                locomotion.movingSpeedFactor = oriSpeedFactor;
            }
            else
            {
                countDowns[onFireIndex].text = ((int)fireCountdown).ToString();
            }
        }
        if (onEle)
        {
            eleCountdown -= Time.fixedDeltaTime;
            if (eleCountdown <= 0f)
            {
                eleCountdown = 0f;
                UpdateState(onEleIndex);
                onEleIndex = -1;
                onEle = false;
                locomotion.movingSpeedFactor = oriSpeedFactor;
            }
            else
            {
                countDowns[onEleIndex].text = ((int)eleCountdown).ToString();
            }
        }
        if (onPoi)
        {
            int retCountdown = (int)poiCountdown;
            poiCountdown -= Time.fixedDeltaTime;
            if ((int)poiCountdown < retCountdown)
            {
                currentHP -= poiDamage;
                currentHP = Mathf.Min(currentHP, maxHP);
                HPBar.value = currentHP / maxHP;
            }
            if (poiCountdown <= 0f)
            {
                poiCountdown = 0f;
                UpdateState(onPoiIndex);
                onPoiIndex = -1;
                onPoi = false;
                locomotion.movingSpeedFactor = oriSpeedFactor;
            }
            else
            {
                countDowns[onPoiIndex].text = ((int)poiCountdown).ToString();
            }
        }
    }

    private void UpdateState(int index)
    {
        for(int i = index + 1; i < states.Length - 1; i++)
        {
            if (i == onFireIndex)
            {
                onFireIndex--;
                states[onFireIndex].sprite = stateImages[0];
                countDowns[onFireIndex].text = ((int)fireCountdown).ToString();
                index++;
            }
            else if(i == onEleIndex)
            {
                onEleIndex--;
                states[onEleIndex].sprite = stateImages[1];
                countDowns[onEleIndex].text = ((int)eleCountdown).ToString();
                index++;
            }
            else if (i == onPoiIndex)
            {
                onPoiIndex--;
                states[onPoiIndex].sprite = stateImages[2];
                countDowns[onPoiIndex].text = ((int)poiCountdown).ToString();
                index++;
            }
        }
        states[index].gameObject.SetActive(false);
        stateIndex--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Gun")
        {
            GunAttached(collision.transform);
        }
    }

    private void GunAttached(Transform gun)
    {
        for(int i = 0; i < gunPack.guns.Length; i++)
        {
            if(gunPack.guns[i].name == gun.name)
            {
                if (holdingGun != null)
                {
                    holdingGun.gameObject.SetActive(false);
                }
                gunPack.guns[i].gameObject.SetActive(true);
                holdingGun = gunPack.guns[i];
                holdingGun.Initialize();
                bulletAmountText.text = holdingGun.currentBulletAmount.ToString();
                SetAttackType();
                SetGunIcon(i);
            }
        }
    }

    private void SetAttackType()
    {
        anim.SetAttackType(holdingGun.gunType, holdingGun.attackType);
    }

    public void SetGunIcon(int index)
    {
        currentGunIcon.SetActive(false);
        if (index >= 0)
        {
            currentGunIcon = gunIcon[index];
            currentGunIcon.SetActive(true);
        }
        else
        {
            holdingGun = null;
        }
    }

    public void Damaged(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0f);
        HPBar.value = currentHP / maxHP;
        if(currentHP / maxHP <= 0.3f)
        {
            fill.sprite = RedHealth;
        }
        else
        {
            fill.sprite = GreenHealth;
        }
    }

    public Vector3 GetRayCastingPosition()
    {
        return transform.position + playerMiddleFromPosition;
    }

    public void SetState(Bullet.Attribute attribute)
    {
        switch (attribute)
        {
            case Bullet.Attribute.Fire:
                if (onFire == false)
                {
                    states[stateIndex].gameObject.SetActive(true);
                    states[stateIndex].sprite = stateImages[0];
                    locomotion.movingSpeedFactor *= 0.8f;
                    onFire = true;
                    onFireIndex = stateIndex;
                    stateIndex++;
                }
                fireCountdown += 5f;
                poiCountdown = Mathf.Round(poiCountdown);
                fireCountdown = Mathf.Min(fireCountdown, 15f);
                break;
            case Bullet.Attribute.Thunder:
                if (onEle == false)
                {
                    states[stateIndex].gameObject.SetActive(true);
                    states[stateIndex].sprite = stateImages[1];
                    locomotion.movingSpeedFactor = 0f;
                    onEle = true;
                    onEleIndex = stateIndex;
                    stateIndex++;
                }
                eleCountdown += 1f;
                eleCountdown = Mathf.Round(eleCountdown);
                eleCountdown = Mathf.Min(eleCountdown, 3f);
                break;
            case Bullet.Attribute.Poison:
                if (onPoi == false)
                {
                    states[stateIndex].gameObject.SetActive(true);
                    states[stateIndex].sprite = stateImages[2];
                    locomotion.movingSpeedFactor *= 0.5f;
                    onPoi = true;
                    onPoiIndex = stateIndex;
                    stateIndex++;
                }
                poiCountdown += 5f;
                poiCountdown = Mathf.Round(poiCountdown);
                poiCountdown = Mathf.Min(poiCountdown, 15f);
                break;
        }
    }
}
