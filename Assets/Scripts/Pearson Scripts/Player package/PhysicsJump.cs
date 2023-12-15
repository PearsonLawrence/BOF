using UnityEngine;

public class PhysicsJump : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float gravityScale = 5;
    [SerializeField] private float fallGravityScale = 15;
    public GameObject particleTest;
    public GameObject spawnPoint;
    public bool isJumping;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.gravityScale = gravityScale;
    }

    public void Jump(bool grounded)
    {
        if (!grounded) return;

        rb.gravityScale = gravityScale;
        float jumpForce = CalculateJumpForce();
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private float CalculateJumpForce()
    {
        return Mathf.Sqrt(jumpHeight * (-9.8f * rb.gravityScale) * -2) * rb.mass;
    }

    public void GravityChange(float pressTime, float pressWindow, bool grounded)
    {
        if (pressTime < pressWindow)
        {
            rb.gravityScale = fallGravityScale;
        }

        if (grounded && isJumping)
        {
            rb.gravityScale = gravityScale;
            isJumping = false;
        }
    }
}
