using UnityEngine;

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
    [SerializeField] private float slowSpeed;
    [SerializeField] private float regSpeed;

    private float speed;
    private Rigidbody2D rb;
    private Vector3 startScale;
    private Vector2 rayFeetPos, rayLeftPos, rayRightPos, rayUpPos;
    public Vector2 currentSurface;
    private MovementStates currentMoveState;

    public float Horz;
    public bool isCrouching;
    public bool isGrounded;
    public bool jumpDelay;
    public bool gravityChanged;
    public bool isSlow;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = regSpeed;
        startScale = transform.localScale;
    }

    public void MoveSpeed(string type)
    {
        switch (type)
        {
            case "reg":
                speed = Mathf.MoveTowards(speed, regSpeed, Time.unscaledDeltaTime * speed);
                break;
            case "slow":
                speed = Mathf.MoveTowards(speed, slowSpeed, Time.unscaledDeltaTime * speed);
                break;
            case "sprint":
                speed = Mathf.MoveTowards(speed, 5, Time.unscaledDeltaTime * speed);
                break;
        }
    }

    private bool GetGrounded(out RaycastHit2D hit)
    {
        UpdateRayPositions();
        RaycastHit2D[] hits = { Physics2D.Raycast(rayFeetPos, -Vector2.up, 1), Physics2D.Raycast(rayLeftPos, -Vector2.right, 1),
                                Physics2D.Raycast(rayRightPos, Vector2.right, 1), Physics2D.Raycast(rayUpPos, Vector2.up, 1) };
        hit = FindClosestHit(hits);

        if (hit)
        {
            if (hit.distance < 0.1f)
            {
                currentSurface = -hit.normal;
                return true;
            }
        }

        hit = new RaycastHit2D();
        return false;
    }

    private RaycastHit2D FindClosestHit(RaycastHit2D[] hits)
    {
        RaycastHit2D closestHit = new RaycastHit2D();
        closestHit.distance = 1f;

        foreach (var hit in hits)
        {
            if (hit && hit.distance < closestHit.distance && hit.transform.gameObject != gameObject)
            {
                closestHit = hit;
            }
        }

        return closestHit;
    }

    private void UpdateRayPositions()
    {
        rayFeetPos = new Vector2(transform.position.x, transform.position.y - (startScale.y + .1f));
        rayUpPos = new Vector2(transform.position.x, transform.position.y + (startScale.y + .1f));
        rayLeftPos = new Vector2(transform.position.x - (startScale.x + .1f), transform.position.y);
        rayRightPos = new Vector2(transform.position.x + (startScale.x + .1f), transform.position.y);
    }
    public void GroundMovement(RaycastHit2D hit)
    {
        //if (rb.velocity.x > 0)
        //{
        //    transform.localScale = new Vector3(.5f, transform.localScale.y, transform.localScale.z);
        //}
        //else if (rb.velocity.x < 0)
        //{
        //    transform.localScale = new Vector3(-.5f, transform.localScale.y, transform.localScale.z);

        //}

        if(gravityChanged && hit.distance < .005f)
        {
            if (hit.distance < .005f && !jumpDelay)
            {
                Vector3 upDirection = hit.normal;
                Vector3 forwardDirection = transform.forward - (Vector3.Dot(transform.forward, upDirection) * upDirection);
                Vector3 rightDirection = Vector3.Cross(upDirection, forwardDirection).normalized;

                // Get horizontal input
                float horizontalInput = Input.GetAxis("Horizontal");

                // Set velocity based on input and right direction
                rb.velocity = rightDirection * horizontalInput * speed;
            }
        }
        else
        {
            if (hit.distance < .1f && hit.normal == Vector2.up)
                rb.velocity = new Vector2(Horz * speed, rb.velocity.y);
        }



        if (hit.distance < .1f)
            transform.up = Vector2.Lerp(transform.up, hit.normal, ((isSlow) ? 45 : 25)  * Time.fixedDeltaTime);

    }
    public void AirMovement(RaycastHit2D hit)
    {

        if (hit.distance < .1f)
            transform.up = Vector2.Lerp(transform.up, hit.normal, ((isSlow) ? 45 : 25) * Time.fixedDeltaTime);

    }
    private void MovementStateMachine(MovementStates state)
    {
        RaycastHit2D hit;
        isGrounded = GetGrounded(out hit);
        state = (isGrounded) ? MovementStates.GROUND_MOVEMENT : MovementStates.AIR_MOVEMENT;

        switch (state)
        {
            case MovementStates.GROUND_MOVEMENT:
                GroundMovement(hit);
                break;
            case MovementStates.AIR_MOVEMENT:
                AirMovement(hit);
                break;
            case MovementStates.WALL_MOVEMENT:
                // Wall movement implementation
                break;
        }
    }
    public void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = (isSlow) ? slowSpeed : regSpeed;
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
