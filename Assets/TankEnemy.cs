using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float raycastDistance = 0.5f;
    public LayerMask obstacleLayer;

    private int directionSign = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveBackAndForth();
    }

    void MoveBackAndForth()
    {
        Vector2 direction = Vector2.right * directionSign;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, obstacleLayer);

        if (hit.collider != null || IsGrounded() )
        {
            directionSign *= -1;

            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, obstacleLayer);

        return groundHit.collider != null;
    }
}
