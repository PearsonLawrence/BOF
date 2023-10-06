using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum SubMovementStates
{
       FLUID_MOVEMENT,
       STEALTH_MOVEMENT,
       CLASSIC_MOVEMENT,
}
public enum MovementStates
{
    GROUND_MOVEMENT,
    AIR_MOVEMENT,
    WALL_MOVEMENT
}
public class PlayerMovementController : MonoBehaviour
{
    private float speed;
    [SerializeField] float slowSpeed;
    [SerializeField] float regSpeed;
    [SerializeField] float transitionSpeed;
    public float Horz;
    public bool isCrouching;
    public bool isGrounded;
    Rigidbody2D rb;
    // Start is called before the first frame update
    bool tempCase;
    MovementStates currentMoveState;
    SubMovementStates currentSubState;
    Ray rayFeet, rayDown, rayLeft, rayRight, rayUp;

    Vector2 rayFeetPos, rayLeftPos, rayRightPos, rayUpPos;

    Vector3 StartScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = regSpeed;
        StartScale = transform.localScale;
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
    public LayerMask LayersToHit;
    public bool getGrounded(out RaycastHit2D hit)
    {


        rayFeetPos = new Vector2(transform.position.x, transform.position.y - (StartScale.y + .1f));
        rayUpPos = new Vector2(transform.position.x, transform.position.y + (StartScale.y + .1f));
        rayLeftPos = new Vector2(transform.position.x - (StartScale.x + .1f), transform.position.y);
        rayRightPos = new Vector2(transform.position.x + (StartScale.x + .1f), transform.position.y);
        
        //rayFeet = new Ray(rayFeetPos, -transform.up);
        //rayUp = new Ray(rayUpPos, transform.up);
        //rayDown = new Ray(transform.position, -Vector2.up);
        //rayLeft = new Ray(rayLeftPos, -transform.right);
        //rayRight = new Ray(rayRightPos, transform.right);

        //RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, 1);
        RaycastHit2D hit2 = Physics2D.Raycast(rayFeetPos, -Vector2.up, 1);
        RaycastHit2D hit3 = Physics2D.Raycast(rayUpPos, Vector2.up, 1);
        RaycastHit2D hit4 = Physics2D.Raycast(rayLeftPos, -Vector2.right, 1);
        RaycastHit2D hit5 = Physics2D.Raycast(rayRightPos, Vector2.right, 1);
        Debug.DrawRay(rayFeetPos, -Vector2.up * .2f, Color.red);
        Debug.DrawRay(rayUpPos, Vector2.up * .2f, Color.red);
        Debug.DrawRay(rayLeftPos, -Vector2.right * .2f, Color.red);
        Debug.DrawRay(rayRightPos, Vector2.right * .2f, Color.red);
        
        if (hit2)
        {
            if (hit2.distance < .5f)
            {

                //Debug.DrawLine(transform.position, hit2.point, Color.green);

                hit = hit2;
                return true;
            }
        }
        if (hit3)
        {
            if (hit3.distance < .5f)
            {
                Debug.Log("cieling");
                // Debug.DrawLine(transform.position, hit3.point, Color.green);
                hit = hit3;
                return true;
            }
        }
        if (hit4)
        {
            if (hit4.distance < .5f)
            {
                //Debug.DrawLine(transform.position, hit4.point, Color.green);
                hit = hit4;
                return true;
            }
        }
        if (hit5)
        {
            if (hit5.distance < .5f)
            {
                //Debug.DrawLine(transform.position, hit5.point, Color.green);
                hit = hit5;
                return true;
            }
        }
        hit = new RaycastHit2D();
        return false;
    }

    public void GroundMovement(RaycastHit2D hit)
    {
        transform.up = Vector2.Lerp(transform.up, hit.normal, 25 * Time.unscaledDeltaTime);
        
        if(Horz > 0)
        {
            transform.localScale = new Vector3(.5f, transform.localScale.y, transform.localScale.z);
        }
        else if(Horz < 0)
        {
            transform.localScale = new Vector3(-.5f, transform.localScale.y, transform.localScale.z);

        }
        if(Horz != 0)
            rb.velocity = new Vector2(Horz * speed, rb.velocity.y);
        



    }
    public void AirMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.up = Vector3.Slerp(transform.up, dir, Time.unscaledDeltaTime * 5);
        }
        else
        {
            if (Horz > 0)
            {
                transform.localScale = new Vector3(.5f, transform.localScale.y, transform.localScale.z);
            }
            else if (Horz < 0)
            {
                transform.localScale = new Vector3(-.5f, transform.localScale.y, transform.localScale.z);

            }
        }
       
    }
    public void MovementStateMachine(MovementStates state)
    {
        RaycastHit2D hit;
        isGrounded = getGrounded(out hit);
        state = (isGrounded) ? MovementStates.GROUND_MOVEMENT : MovementStates.AIR_MOVEMENT;
        switch(state)
        {
            case MovementStates.GROUND_MOVEMENT:
                GroundMovement(hit);
                break;
            case MovementStates.AIR_MOVEMENT:
                AirMovement();
                break;
            case MovementStates.WALL_MOVEMENT:
                break;
        }
    }

    public void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Horz = Input.GetAxis("Horizontal");
        MovementStateMachine(currentMoveState);
        //if (!tempCase)
        //{
        //    Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //    transform.up = Vector3.Slerp(transform.up, dir, Time.unscaledDeltaTime * 10);
        //}
        //else
        //{
        //    transform.right = new Vector2(transform.forward.x * Horz, 0);
        //    rb.velocity = new Vector2(Horz * speed, rb.velocity.y);
        //}

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
