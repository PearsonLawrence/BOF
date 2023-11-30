using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDamageComponent : MonoBehaviour
{
    public HealthComponent owner;
    public float regularDamage, heavyDamage, buffedDamage, currentDamage;
    public bool isDestroyedOnHit;
    public bool isAttacking;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!owner) return;
        if (!isAttacking || owner.gameObject == collision.gameObject ||collision.gameObject.CompareTag("Bullet")) return;
        
        HealthComponent hc = collision.gameObject.GetComponent<HealthComponent>();
        if (hc)
        {
            if (hc != owner)
            {
                hc.takeDamage(currentDamage);
            }
        }

        if (isDestroyedOnHit && owner)
        {
            if (owner.gameObject.CompareTag("Boss"))
            {

                this.gameObject.SetActive(false);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else if(isDestroyedOnHit)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttacking) return;

        HealthComponent hc = collision.gameObject.GetComponent<HealthComponent>();
        if (hc)
        {
            if (hc != owner)
            {
                hc.takeDamage(currentDamage);
                
            }
        }
        if (isDestroyedOnHit)
        {
            if (owner.gameObject.CompareTag("Boss"))
            {

                this.gameObject.SetActive(false);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }


}
