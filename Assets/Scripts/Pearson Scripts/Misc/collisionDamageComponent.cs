using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDamageComponent : MonoBehaviour
{
    public HealthComponent owner;
    public float regularDamage, heavyDamage, buffedDamage, currentDamage;
    public float knockbackForce;
    public bool isDestroyedOnHit;
    public bool isAttacking;
    public bool isKnockback;
    [SerializeField] bool isPlayer;
    [SerializeField] EnemyType bulletType;
    [SerializeField] private GameObject impactPrefab;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!owner) return;
        string tag = collision.gameObject.tag;


        if (!isAttacking || owner.gameObject == collision.gameObject || tag == "Bullet" || tag == "PlayerHand") return;

        if (isPlayer && tag == "PlayerHand" || isPlayer && tag == "Shield" || isPlayer && tag == "Player" || isPlayer && tag == "Bullet") return;

        HealthComponent hc = collision.gameObject.GetComponent<HealthComponent>();
        PlayerShieldComponent shield = collision.gameObject.GetComponent<PlayerShieldComponent>();
        if (hc)
        {
            if (hc != owner)
            {
                hc.takeDamage(currentDamage);

            }
            if (isKnockback)
            {
                Rigidbody2D tempRB = hc.getRBRef();
                if (tempRB != null)
                {
                    Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    tempRB.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }

        if (shield)
            shield.addBulletToCollection();

       
        if (impactPrefab)
        {
            GameObject temp = Instantiate(impactPrefab, transform.position, Quaternion.identity);
            temp.transform.forward = -transform.up;
            Destroy(temp, 1);
        }
        if (isDestroyedOnHit && owner)
        {
            if (owner.gameObject.CompareTag("Boss"))
                this.gameObject.SetActive(false);
            else
                Destroy(this.gameObject);
        }
        else if(isDestroyedOnHit)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!owner) return;
        //if (!isAttacking || owner.gameObject == collision.gameObject || collision.gameObject.CompareTag("Bullet")) return;

        //HealthComponent hc = collision.gameObject.GetComponent<HealthComponent>();
        //if (hc)
        //{
        //    if (hc != owner)
        //    {
        //        hc.takeDamage(currentDamage);
        //    }
        //}

        //if (isDestroyedOnHit && owner)
        //{
        //    if (owner.gameObject.CompareTag("Boss"))
        //    {

        //        this.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
        //else if (isDestroyedOnHit)
        //    Destroy(this.gameObject);
    }


}
