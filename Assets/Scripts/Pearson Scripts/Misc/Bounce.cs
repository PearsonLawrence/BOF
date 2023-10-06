using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody2D rb;
    public float amount;
    public float min = 500;
    public float max = 1000;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        amount = Random.Range(min, max);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y < this.transform.position.y) 
            rb.AddForce(Vector2.up * amount);
    }

    private void FixedUpdate()
    {
        
    }
    
}
