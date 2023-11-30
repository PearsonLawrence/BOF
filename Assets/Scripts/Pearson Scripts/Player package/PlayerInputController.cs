using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private TimeController timeController;

    [SerializeField]
    private PlayerMovementController playerMovement;
    [SerializeField]
    private PlayerCombatComponent playerCombat;

    [SerializeField]
    private PhysicsJump physicsJump;

    private Rigidbody2D rb;

    public float MinSlow = 0.01f;
    public float Speed = 100;

    public Vector2 TempGravity;
    private float goalSlow;

    private float buttonPressTime;
    private const float ButtonPressWindow = 0.25f;
    private string speedType;
    public bool IsRunning { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeController = GetComponent<TimeController>();
    }

    public PlayerCombatComponent GetPlayerCombatComponent()
    {
        return playerCombat;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleTimeControl();
        HandleJumpInput();

        if(Input.GetMouseButtonDown(0))
        {
            playerCombat.doAttack();
        }
    }

    private void HandleMovementInput()
    {
        IsRunning = playerMovement.Horz != 0;
        playerMovement.isSlow = Input.GetKey(KeyCode.Space);
        playerMovement.isCrouching = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerMovement.gravityChanged = true;
            TempGravity = playerMovement.currentSurface * 9.8f;
            Physics2D.gravity = TempGravity;
        }
        else
        {
            playerMovement.gravityChanged = false;
            Physics2D.gravity = Vector2.down * 9.8f;
        }
    }

    private void HandleTimeControl()
    {
        goalSlow = Input.GetKey(KeyCode.Space) ? MinSlow : 1;
        speedType = Input.GetKey(KeyCode.Space) ? "slow" : "reg";

        if (timeController.currentScale != goalSlow)
        {
            timeController.currentScale = Mathf.MoveTowards(timeController.currentScale, goalSlow, Time.unscaledDeltaTime * Speed);
            timeController.fixedTime = Mathf.MoveTowards(timeController.fixedTime, 0.02f, Time.unscaledDeltaTime * Speed);
            timeController.ChangeTimeScale();
            playerMovement.MoveSpeed(speedType);
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && playerMovement.isGrounded && !playerMovement.jumpDelay)
        {
            buttonPressTime = 0;
            playerMovement.jumpDelay = true;
            physicsJump.Jump(playerMovement.isGrounded);
        }

        buttonPressTime += Time.unscaledDeltaTime;
        if (buttonPressTime > ButtonPressWindow)
        {
            playerMovement.jumpDelay = false;
        }

        if (physicsJump.isJumping)
        {
            physicsJump.GravityChange(buttonPressTime, ButtonPressWindow, playerMovement.isGrounded);
        }
    }
}