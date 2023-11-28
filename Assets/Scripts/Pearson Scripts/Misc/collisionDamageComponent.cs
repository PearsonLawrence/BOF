using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDamageComponent : MonoBehaviour
{
    HealthComponent owner;
    public float regularDamage, heavyDamage, buffedDamage, currentDamage;
    public bool isDestroyedOnHit;
    public bool isAttacking;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttacking) return;

        HealthComponent hc = collision.gameObject.GetComponent<HealthComponent>();
        if (hc)
        {
            if (hc != owner)
            {
                hc.takeDamage(currentDamage);
                if (isDestroyedOnHit)
                    Destroy(this.gameObject);
            }
        }
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
                if (isDestroyedOnHit)
                    Destroy(this.gameObject);
            }
        }
    }


}
