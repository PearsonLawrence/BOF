using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerComponent : MonoBehaviour
{
    [SerializeField] StarBoss boss;
    [SerializeField] Collider2D col;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (!collision.gameObject.CompareTag("Player")) return;

        boss.InitiateBossFight();
        col.enabled = false;
        this.enabled = false;
    }
}
