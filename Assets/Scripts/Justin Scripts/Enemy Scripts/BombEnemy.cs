using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool groundDetected;
    private bool wallDetected;
    
    [SerializeField]
    private Transform groundPosCheck;
    [SerializeField]
    private Transform wallPosCheck;
    
    public float groundCheckDistance;
    public float wallCheckDistance;
    [SerializeField]
    private LayerMask groundLayer;
    private bool hasTurned;
    private float zAxisAdd;
    public float zAxisAngle;
    private int direction;
    public float groundOffset;
    public float wallOffset;
    public float speed;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasTurned = false;
        direction = 1;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckGroundOrWall();
        Movement();
    }

    void CheckGroundOrWall()
    {
        groundDetected = Physics2D.Raycast(groundPosCheck.position, -transform.up, groundCheckDistance, groundLayer);
        wallDetected = Physics2D.Raycast(wallPosCheck.position, transform.right, wallCheckDistance, groundLayer);

        if(!groundDetected)
        {
            if(hasTurned == false)
            {
                zAxisAdd -= zAxisAngle;
                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);
                
                if(direction == 1)
                {
                    transform.position = new Vector2(transform.position.x + groundOffset, transform.position.y - groundOffset);
                    hasTurned = true;
                    direction = 2;
                }
                
                if(direction == 2)
                {
                    transform.position = new Vector2(transform.position.x - groundOffset, transform.position.y - groundOffset);
                    hasTurned = true;
                    direction = 3;
                }
                
                if(direction == 3)
                {
                    transform.position = new Vector2(transform.position.x - groundOffset, transform.position.y + groundOffset);
                    hasTurned = true;
                    direction = 4;
                }

                if (direction == 4)
                {
                    transform.position = new Vector2(transform.position.x + groundOffset, transform.position.y + groundOffset);
                    hasTurned = true;
                    direction = 1;
                }

            }
        }

        if(groundDetected)
        {
            hasTurned = false;
        }

        if(wallDetected)
        {
            if(hasTurned == false)
            {
                zAxisAdd += zAxisAngle;
                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);
                if(direction == 1)
                {
                    transform.position = new Vector2(transform.position.x - wallOffset , transform.position.y + wallOffset);
                    hasTurned = true;
                    direction = 4;
                }
                
                if(direction == 2)
                {
                    transform.position = new Vector2(transform.position.x + wallOffset, transform.position.y + wallOffset);
                    hasTurned = true;
                    direction = 1;
                }
                
                if(direction == 3)
                {
                    transform.position = new Vector2(transform.position.x + wallOffset, transform.position.y - wallOffset);
                    hasTurned = true;
                    direction = 2;
                }
                
                if(direction == 4)
                {
                    transform.position = new Vector2(transform.position.x - wallOffset, transform.position.y - wallOffset);
                    hasTurned = true;
                    direction = 3;
                }
            }
        }
    }

    void Movement()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundPosCheck.position, new Vector2(groundPosCheck.position.x, groundPosCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallPosCheck.position, new Vector2(wallPosCheck.position.x + wallCheckDistance, wallPosCheck.position.y));
    }
}

