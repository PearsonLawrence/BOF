using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private float speed;
    [SerializeField] float slowSpeed;
    [SerializeField] float regSpeed;
    [SerializeField] float transitionSpeed;
    public float Horz;
    public bool isCrouching;
    Rigidbody2D rb;
    // Start is called before the first frame update
    bool tempCase;

    public void OnCollisionStay2D(Collision2D collision)
    {
        tempCase = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        tempCase = false;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = regSpeed;
    }
    public void moveSpeed(string type)
    {
        switch(type)
        {
            case "reg":
                speed = Mathf.MoveTowards(speed, regSpeed, Time.unscaledDeltaTime * speed); ;
                break;
            case "slow":
                speed = Mathf.MoveTowards(speed, slowSpeed, Time.unscaledDeltaTime * speed);
                break;
            case "sprint":
                speed = Mathf.MoveTowards(speed, 5, Time.unscaledDeltaTime * speed);
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Horz = Input.GetAxis("Horizontal");
        if (!tempCase)
        {
            Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.up = Vector3.Slerp(transform.up, dir, Time.unscaledDeltaTime * 10);
        }
        else
        {
            transform.right = new Vector2(transform.forward.x * Horz, 0);
            rb.velocity = new Vector2(Horz * speed, rb.velocity.y);
        }
        
        if (isCrouching && transform.localScale != new Vector3(.5f,.25f, 1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .25f, transform.position.z);
            transform.localScale = new Vector3(.5f, .25f, 1);
        }
        else if(!isCrouching && transform.localScale == new Vector3(.5f, .25f, 1))
        {
            transform.localScale = new Vector3(.5f, .5f, 1);
        }


    }
}
