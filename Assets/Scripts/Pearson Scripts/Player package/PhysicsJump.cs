using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsJump : MonoBehaviour
{
    [SerializeField] float jumpHeight = 5;

    [SerializeField] float gravityScale = 5;
    [SerializeField] float fallGravityScale = 15;
    public GameObject particleTest;
    public GameObject spawnPoint;
    public bool isJumping;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    public void Jump(bool grounded)
    {
        Vector2 FeetPos = new Vector2(transform.position.x, transform.position.y - (.5f));
        rb.gravityScale = gravityScale;
        float jumpForce = Mathf.Sqrt(jumpHeight  * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        Instantiate<GameObject>(particleTest, spawnPoint.transform.position, new Quaternion());
        //if (rb.velocity.y > 0)
        //{
        //    rb.gravityScale = gravityScale;
        //}
        //else if (rb.velocity.y < 0 && !grounded)
        //{
        //    rb.gravityScale = fallGravityScale;
        //}
    }

    public void gravityChange(float pTime, float pWindow, bool grounded)
    {
        if (pTime < pWindow)
        {
            //cancel the jump
            rb.gravityScale = fallGravityScale;
        }
        if (grounded && isJumping)
        {
            rb.gravityScale = gravityScale;
            isJumping = false;
        }
    }


}
