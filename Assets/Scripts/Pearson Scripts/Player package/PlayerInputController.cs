using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public bool isSlowMo;

    [SerializeField]
    private TimeController timeController;

    [SerializeField]
    private PlayerMovementController playerMovement;
    [SerializeField]
    private PhysicsJump physJump;

    private Rigidbody2D rb;

    public float minSlow = .01f;
    public float speed = 100;
    public Vector2 tempGrav;
    private float goalSlow;

    float buttonPressTime;
    float buttonPressWindow;

    string speedType;

    // Start is called before the first frame update
    void Start()
    {
        timeController = this.GetComponent<TimeController>();
        playerMovement = this.GetComponent<PlayerMovementController>();
        physJump = this.GetComponent<PhysicsJump>();
        rb = this.GetComponent<Rigidbody2D>();
    }


    bool isRunning;
    // Update is called once per frame
    void Update()
    {
        isRunning = (playerMovement.Horz != 0);
        goalSlow = (Input.GetKey(KeyCode.Space)) ? minSlow : 1;
        speedType = (Input.GetKey(KeyCode.Space)) ? "slow" : "reg";
        playerMovement.isSlow = Input.GetKey(KeyCode.Space);

        if (timeController.currentScale != goalSlow)
        {
            timeController.currentScale = Mathf.MoveTowards(timeController.currentScale, goalSlow, Time.unscaledDeltaTime * speed);
            timeController.fixedTime = Mathf.MoveTowards(timeController.fixedTime, .02f, Time.unscaledDeltaTime * speed);
            timeController.ChangeTimeScale();
            playerMovement.moveSpeed(speedType);
         //   Debug.Log("test");
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerMovement.gravityChanged = true;
            tempGrav = playerMovement.currentSurface * 9.8f;
            Physics2D.gravity = tempGrav;
        }
        else
        {
            playerMovement.gravityChanged = false;
            Physics2D.gravity = new Vector2(0, -9.8f);

        }

        if (Input.GetKey(KeyCode.S))
        {
            playerMovement.isCrouching = true;
        }
        else
        {
            playerMovement.isCrouching = false;
        }
        
        if (Input.GetKeyDown(KeyCode.W) && playerMovement.isGrounded && playerMovement.jumpDelay == false)
        {
            buttonPressTime = 0;
            playerMovement.jumpDelay = true;
            physJump.Jump(playerMovement.isGrounded);
        }
        buttonPressTime += Time.unscaledDeltaTime;
        if(buttonPressTime > .25f)
        {
            playerMovement.jumpDelay = false;
        }

        if (physJump.isJumping)
        {
            

            physJump.gravityChange(buttonPressTime, buttonPressWindow, playerMovement.isGrounded);
        }

    }
}
