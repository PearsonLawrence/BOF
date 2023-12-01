using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeCombatComponent : MonoBehaviour
{
    private TankEnemy mainBody;
    [SerializeField] private GameObject meleeBase;
    [SerializeField] private GameObject meleeHitBox;

    private GameObject TargetObj;
    [SerializeField] private float attackFreq = 3;
    [SerializeField] private float attackDist = 4;
    [SerializeField] private float armRotateSpeed = 4;
    [SerializeField] private collisionDamageComponent damageComponent;
    private float frequency;
    private float targetDist;
    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        mainBody = GetComponent<TankEnemy>();
        TargetObj = mainBody.Player;
        frequency = attackFreq;
    }

    // Update is called once per frame
    public void ResetAttackValues()
    {
        isAttacking = false;
        mainBody.GetAnimator().SetBool("isAttacking", isAttacking);
        frequency = attackFreq;
        damageComponent.isAttacking = isAttacking;

    }
    void Update()
    {
        frequency -= Time.deltaTime;
        targetDist = Vector2.Distance(transform.position, TargetObj.transform.position);
       
        if(targetDist < attackDist)
        {
            Vector2 direction = -(transform.position - (Vector3)TargetObj.transform.position).normalized;
            meleeBase.transform.up = Vector3.Lerp(meleeBase.transform.up, direction, armRotateSpeed * Time.deltaTime);
            Vector3 rayDir = TargetObj.transform.position - meleeHitBox.transform.position;

            //RaycastHit2D PlayerAimRay = Physics2D.Raycast(meleeHitBox.transform.position, rayDir, 10000);
           
            if (frequency <= 0 && !isAttacking )
            {
                isAttacking = true;
                mainBody.GetAnimator().SetBool("isAttacking", isAttacking);
                damageComponent.isAttacking = isAttacking;
            }    
        }
        else
        {
            if(meleeBase.transform.up != transform.up)
                meleeBase.transform.up = Vector3.Lerp(meleeBase.transform.up, transform.up, armRotateSpeed * Time.deltaTime);
        }
    }
}
