using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRoomCollider : MonoBehaviour
{
    [SerializeField]
    private float timer = 10;

    [SerializeField]
    private BoxCollider2D boxCollider;


    void Update()
    {
        if (timer < 0) return;

        timer -= Time.deltaTime;

        if (timer <= 0) boxCollider.enabled = false;

    }
}
