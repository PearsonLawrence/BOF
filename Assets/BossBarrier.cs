using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    PolygonCollider2D barrier;
    // Start is called before the first frame update
    void Start()
    {
        barrier = GetComponent<PolygonCollider2D>();
        barrier.enabled = true;
    }

    public void BarrierDown()
    {
        if (barrier != null)
        {
            barrier.enabled = false;
        }

    }
}
