using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        isMoving = (player.Horz != 0);
        anim.SetBool("isMoving", isMoving);
        
    }
}
