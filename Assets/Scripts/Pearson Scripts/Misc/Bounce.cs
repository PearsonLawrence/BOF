using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody2D rb;
    public float amount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        amount = Random.Range(80, 500);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * amount);
        }
    }
}
