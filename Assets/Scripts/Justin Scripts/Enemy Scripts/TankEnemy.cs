using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public float speed;
    public float empRadius = 5f;
    public float empForce = 10f;

    private bool movingRight = true;
    public Transform groundDetection;

    private void Start()
    {
        TriggerEmpPulse();
    }


    // Update is called once per frame
    void Update() // add wall detection after alpha
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        

        if (groundInfo.collider == false)
        {
            if(movingRight == true)
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

    private void TriggerEmpPulse()
    {
        Debug.Log("EMP Pulse triggered.");
        
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, empRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Detected!");
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Vector2 empDirection = (collider.transform.position - transform.position).normalized;
                    rb.AddForce(empDirection * empForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, empRadius);
    }



}
