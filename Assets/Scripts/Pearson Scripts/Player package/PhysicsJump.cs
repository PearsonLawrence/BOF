using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsJump : MonoBehaviour
{
    [SerializeField] float jumpHeight = 5;

    [SerializeField] float gravityScale = 5;
    [SerializeField] float fallGravityScale = 15;

    public bool isJumping;
    float buttonPressTime;
    float buttonPressWindow;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.gravityScale = gravityScale;
            float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (rb.velocity.y > 0)
            {
                rb.gravityScale = gravityScale;
            }
            else
            {
                rb.gravityScale = fallGravityScale * Time.timeScale;
            }
        
            
        }

        if(isJumping)
        {
            buttonPressTime += Time.unscaledDeltaTime;

            if (buttonPressTime < buttonPressWindow)
            {
                //cancel the jump
                rb.gravityScale = fallGravityScale;
            }
            if(rb.velocity.y < 0)
            {
                rb.gravityScale = fallGravityScale;
                isJumping = false;
            }
        }
    }


}
