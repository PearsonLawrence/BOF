using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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

    public bool isSlow;

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
   
        RaycastHit2D hit2 = Physics2D.Raycast(rayFeetPos, -Vector2.up, 1);
        RaycastHit2D hit3 = Physics2D.Raycast(rayUpPos, Vector2.up, 1);
        RaycastHit2D hit4 = Physics2D.Raycast(rayLeftPos, -Vector2.right, 1);
        RaycastHit2D hit5 = Physics2D.Raycast(rayRightPos, Vector2.right, 1);

        Debug.DrawRay(rayFeetPos, -Vector2.up * .2f, Color.red);
        Debug.DrawRay(rayUpPos, Vector2.up * .2f, Color.red);
        Debug.DrawRay(rayLeftPos, -Vector2.right * .2f, Color.red);
        Debug.DrawRay(rayRightPos, Vector2.right * .2f, Color.red);

        RaycastHit2D closestHit = new RaycastHit2D();
        closestHit.distance = 1;
        if (hit2)
            closestHit = (hit2.distance < closestHit.distance) ? hit2 : closestHit;
        if (hit3)
            closestHit = (hit3.distance < closestHit.distance) ? hit3 : closestHit;
        if (hit4)
            closestHit = (hit4.distance < closestHit.distance) ? hit4 : closestHit;
        if (hit5)
            closestHit = (hit5.distance < closestHit.distance) ? hit5 : closestHit;

        if (closestHit)
        {
            Debug.Log("??");
            if (closestHit.distance < .2f)
            {
                Debug.Log("touch");
                hit = closestHit;
                return true;
            }
        }

        hit = new RaycastHit2D();
        return false;
    }

    public void GroundMovement(RaycastHit2D hit)
    {
        
        if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(.5f, transform.localScale.y, transform.localScale.z);
        }
        else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-.5f, transform.localScale.y, transform.localScale.z);

        }
        if(hit.distance < .1f && hit.normal == Vector2.up)
            rb.velocity = new Vector2(Horz * speed, rb.velocity.y);

        if(hit.distance < .1f)
            transform.up = Vector2.Lerp(transform.up, hit.normal, ((isSlow) ? 40 : 20)  * Time.fixedDeltaTime);


    }
    public void AirMovement(RaycastHit2D hit)
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
        if (hit.distance < .1f)
            transform.up = Vector2.Lerp(transform.up, hit.normal, ((isSlow) ? 50 : 25) * Time.fixedDeltaTime);
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
                AirMovement(hit);
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
