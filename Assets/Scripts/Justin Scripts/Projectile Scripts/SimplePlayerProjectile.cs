using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerProjectile : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public int damage = 1;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
