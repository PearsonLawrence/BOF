using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Circle,
    Square,
    Triangle,
    Question,
    TimeBoss,
    Boss
}

public class PowerupDropComponent : MonoBehaviour
{
    [SerializeField] EnemyType type;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] float buffTime;
    [SerializeField] private GameObject sfx;
    [SerializeField] private bool isPlayerPart;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            if (sfx != null)
            {
                GameObject tempSound = Instantiate(sfx, transform.position, Quaternion.identity);
                Destroy(tempSound, 2);
            }

            PlayerCombatComponent combat = collision.gameObject.GetComponent<PlayerInputController>().GetPlayerCombatComponent();
            if (combat == null) return;

            if (pickupEffect != null) Instantiate(pickupEffect, transform.position, Quaternion.identity);

            if(!isPlayerPart)
            {
                combat.SetCurrentPowerupType(type);
                combat.buffTime = buffTime;
                Destroy(this.gameObject);
            }
            else
            {
                HealthComponent tempHC = collision.gameObject.GetComponent<HealthComponent>();
                if (tempHC) tempHC.DoHeal(buffTime);

                Destroy(this.gameObject);
            }
        }
    }
}
