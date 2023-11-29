using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public float speed;
    public float reflectProjectileSpeed = 5f;
    public LayerMask reflectableLayer;
    

    private bool movingRight = true;
    public Transform groundDetection;

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update() // add wall detection after alpha
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);


        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }

            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((reflectableLayer.value & 1 << other.gameObject.layer) != 0)
        {
            Rigidbody2D projectileRb = other.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                ReflectProjectile(projectileRb);
            }
        }
    }

    private void ReflectProjectile(Rigidbody2D projectileRb)
    {
        Vector2 reflectedVelocity = -projectileRb.velocity;

        projectileRb.velocity = reflectedVelocity.normalized * reflectProjectileSpeed;
    }








}
