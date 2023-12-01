using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public float speed;
    public float knockbackForce = 5f;
    //public LayerMask reflectableLayer;
    

    private bool movingRight = true;
    public Transform groundDetection;
    public Transform groundDetection2;
    [SerializeField] private Animator anim;
    [SerializeField] private HealthComponent HC;
    public Rigidbody2D rb;
    public GameObject Player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        HC = GetComponent<HealthComponent>();
        HC.setDamageAnimator(anim);
    }
    [SerializeField]
    private float RayDist = .1f;

    public Vector2 dir = Vector2.right;
    // Update is called once per frame

    public Animator GetAnimator()
    {
        return anim;
    }

    void Update() // add wall detection after alpha
    {
        rb.velocity = new Vector2(dir.x * speed * Time.deltaTime, rb.velocity.y);

        RaycastHit2D groundInfoRight = Physics2D.Raycast(groundDetection.position, Vector2.down, RayDist);
        RaycastHit2D wallInfoRight = Physics2D.Raycast(groundDetection.position, Vector2.right, RayDist);
        RaycastHit2D groundInfo2Left = Physics2D.Raycast(groundDetection2.position, Vector2.down, RayDist);
        RaycastHit2D wallInfoLeft = Physics2D.Raycast(groundDetection2.position, -Vector2.right, RayDist);


        if (!groundInfoRight.collider && groundInfo2Left.collider)
        {
            dir = -Vector2.right;
        }
        else if(groundInfoRight.collider && !groundInfo2Left.collider)
        {
            dir = Vector2.right;
        }
        else if (groundInfoRight.collider && groundInfo2Left.collider && wallInfoRight.collider && !wallInfoLeft.collider)
        {
            dir = -Vector2.right;
        }
        else if (groundInfoRight.collider && groundInfo2Left.collider && !wallInfoRight.collider && wallInfoLeft.collider)
        {
            dir = Vector2.right;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;   
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate knockback direction
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

            // Apply force to the player
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            Debug.Log("Player knocked back!");
        }
    }



}








