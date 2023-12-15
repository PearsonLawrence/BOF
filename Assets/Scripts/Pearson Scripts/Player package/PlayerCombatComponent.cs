using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatComponent : MonoBehaviour
{
    public PlayerMovementController player;
    public bool isAttacking;
    public bool isCombo;
    public bool isMoving;
    public int attackNum = 0;
    private Vector3 mousePos;
    public Animator anim;
    public collisionDamageComponent Hand1, Hand2;
    //  [SerializeField] private 
    [SerializeField] private HealthComponent HC;
    [SerializeField] private PlayerShieldComponent shield;
    [SerializeField] private TrailRenderer trail1, trail2, trail3, trail4;
    [SerializeField] private SpriteRenderer body, body2, body3, hand1, hand2, ShieldRender;
    [SerializeField] private Color colorshield, colorshield2;
    public float buffTime;

    private EnemyType currentPowerupType;
    public TMP_Text buffText;

    public void SetCurrentPowerupType(EnemyType type)
    {
        ResetBuff();
        currentPowerupType = type;
    }
    public EnemyType GetCurrentPowerupType()
    {
        return currentPowerupType;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public HealthComponent GetHealthComponent()
    {
        return HC;
    }

    public void doAttack()
    {
        
        attackNum++;
       
        if(shield.GetBulletCollection() <= shield.GetMaxBulletCollection() && shield.GetBulletCollection() > 0 && shield.isBlock)
        {
            attackNum = 1;
            anim.SetBool("isAttacking", true);
        }
        
    }

    public void ShieldShoot()
    {
        shield.shootProjectile();
    }


    public void UpdateCombo()
    {
        if(attackNum > 1)
        {
            attackNum = 1;
        }
        else
        {
            attackNum = 0;
        }
    }
    public void doBlock()
    {
        if(!shield.isBlock && shield.GetShieldCoolDownTimer() <= 0)
        {
            shield.isBlock = true;
            shield.gameObject.SetActive(shield.isBlock);
            anim.SetBool("isBlocking", shield.isBlock);
        }
    }
    public void ShieldShootEnd()
    {
        endBlock();
        shield.ResetShieldCooldown() ;

    }
    public void endBlock()
    {
        if (!shield.isBlock) return;
        attackNum = 0;
        shield.isBlock = false;
        shield.gameObject.SetActive(shield.isBlock);
        anim.SetBool("isBlocking", shield.isBlock);
        anim.SetBool("isAttacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(shield.GetShieldCoolDownTimer() > -.1f) shield.UpdateShieldCooldownTimer();

        if(shield.GetBulletCollection() > shield.GetMaxBulletCollection())
        {
            shield.destroyShield();
        }

        if (buffTime > 0)
        {
            buffTime -= Time.deltaTime;
            buffText.text = "BuffTime: " + buffTime.ToString();
        }
        else
        {
            buffText.text = "No Buff";
        }


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector3)transform.position).normalized;

        isAttacking = (attackNum > 0);
        anim.SetBool("isAttacking", isAttacking);
        Hand1.isAttacking = isAttacking;
        Hand2.isAttacking = isAttacking;

        if (isAttacking || shield.isBlock)
        {
            transform.right = direction;
        }
        else
        {
            transform.up = player.transform.up;
        }
        
        if(buffTime <= 0)
        {
            ResetBuff();
        }
        else
        {
            doPowerUp(currentPowerupType);
        }

        isMoving = (player.Horz != 0);
        anim.SetBool("isMoving", isMoving);
        
    }
    public void doPowerUp(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Circle:
                Hand1.currentDamage = Hand1.buffedDamage;
                Hand2.currentDamage = Hand2.buffedDamage;
                body.color = Color.red;
                body2.color = Color.magenta;
                body3.color = Color.magenta;
                hand1.color = Color.magenta;
                hand2.color = Color.magenta;
                ShieldRender.color = Color.red;
                trail1.startColor = Color.red;
                trail2.startColor = Color.red;
                trail3.startColor = Color.red;
                trail4.startColor = Color.red;
                trail1.endColor = Color.magenta;
                trail2.endColor = Color.magenta;
                trail3.endColor = Color.magenta;
                trail4.endColor = Color.magenta;
                break;
            case EnemyType.Square:
                HC.canTakeDamage = false;
                body.color = Color.green;
                body2.color = Color.yellow;
                body3.color = Color.yellow;
                hand1.color = Color.yellow;
                hand2.color = Color.yellow;
                ShieldRender.color = Color.green;
                trail1.startColor = Color.green;
                trail2.startColor = Color.green;
                trail3.startColor = Color.green;
                trail4.startColor = Color.green;
                trail1.endColor = Color.yellow;
                trail2.endColor = Color.yellow;
                trail3.endColor = Color.yellow;
                trail4.endColor = Color.yellow;
                break;
            case EnemyType.Triangle:
                shield.isPoweredUp = true;
                body.color = Color.blue;
                body2.color = Color.cyan;
                body3.color = Color.cyan;
                hand1.color = Color.cyan;
                hand2.color = Color.cyan;

                ShieldRender.color = Color.magenta;
                trail1.startColor = Color.blue;
                trail2.startColor = Color.blue;
                trail3.startColor = Color.magenta;
                trail4.startColor = Color.magenta;
                trail1.endColor = Color.cyan;
                trail2.endColor = Color.cyan;
                trail3.endColor = Color.blue;
                trail4.endColor = Color.blue;
                break;
            case EnemyType.Question:
                int tempRand = Random.Range(0, 3);
                EnemyType newType = (tempRand == 0) ? EnemyType.Circle : (tempRand == 1) ? EnemyType.Square : EnemyType.Triangle;
                doPowerUp(newType);
                break;
        }
    }
    public void ResetBuff()
    {
        Hand1.currentDamage = Hand1.regularDamage;
        Hand2.currentDamage = Hand2.regularDamage;
        shield.isPoweredUp = false;
        HC.canTakeDamage = true; 
        body.color = Color.white;
        body2.color = Color.white;
        body3.color = Color.white;
        hand1.color = Color.white;
        hand2.color = Color.white;
        ShieldRender.color = Color.white;
        trail1.startColor = Color.white;
        trail2.startColor = Color.white;
        trail3.startColor = Color.white;
        trail4.startColor = Color.white;
        trail1.endColor = Color.white;
        trail2.endColor = Color.white;
        trail3.endColor = Color.white;
        trail4.endColor = Color.white;

    }
}
