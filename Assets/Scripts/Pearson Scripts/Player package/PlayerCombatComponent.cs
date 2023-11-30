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
    public bool isBlock;
    public bool isMoving;
    public int attackNum = 0;
    private Vector3 mousePos;
    public Animator anim;
    public collisionDamageComponent Hand1, Hand2;
    public float buffTime;

    private EnemyType currentPowerupType;
    public TMP_Text buffText;

    public void SetCurrentPowerupType(EnemyType type)
    {
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

    public void doAttack()
    {
        if(!isBlock)
            attackNum++;
        else
        {
            attackNum = 0;
        }
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

    }

    // Update is called once per frame
    void Update()
    {
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
        if (attackNum > 0)
        {
            transform.right = direction;
        }
        else
        {
            transform.up = player.transform.up;
        }

        if(buffTime <= 0)
        {
            Hand1.currentDamage = Hand1.regularDamage;
            Hand2.currentDamage = Hand2.regularDamage;
        }
        else
        {
            Hand1.currentDamage = Hand1.buffedDamage;
            Hand2.currentDamage = Hand2.buffedDamage;
        }

        isMoving = (player.Horz != 0);
        anim.SetBool("isMoving", isMoving);
        
    }
}
