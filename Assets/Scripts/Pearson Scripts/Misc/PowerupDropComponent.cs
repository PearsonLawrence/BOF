using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Circle,
    Square,
    Triangle
}

public class PowerupDropComponent : MonoBehaviour
{
    [SerializeField] EnemyType type;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] float buffTime;
    [SerializeField] private GameObject sfx;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            if (sfx != null) Instantiate(sfx, transform.position, Quaternion.identity);
            PlayerCombatComponent combat = collision.gameObject.GetComponent<PlayerInputController>().GetPlayerCombatComponent();
            if (combat == null) return;

            if (pickupEffect != null) Instantiate(pickupEffect, transform.position, Quaternion.identity);

            combat.SetCurrentPowerupType(type);
            combat.buffTime = buffTime;
            Destroy(this.gameObject);
        }
    }
}
