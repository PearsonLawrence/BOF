using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupTypes
{
    Circle,
    Square,
    Triangle
}

public class PowerupDropComponent : MonoBehaviour
{
    [SerializeField] PowerupTypes type;
    [SerializeField] float buffTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerCombatComponent combat = collision.gameObject.GetComponent<PlayerInputController>().GetPlayerCombatComponent();
            if (combat == null) return;


            combat.SetCurrentPowerupType(type);
            combat.buffTime = buffTime;
            Destroy(this.gameObject);
        }
    }
}