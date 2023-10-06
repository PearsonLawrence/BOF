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

    public float minSlow = .01f;
    public float speed = 100;

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
    }



    // Update is called once per frame
    void Update()
    {
       
        goalSlow = (Input.GetKey(KeyCode.Space)) ? minSlow : 1;
        speedType = (Input.GetKey(KeyCode.Space)) ? "slow" : "reg";


        if (timeController.currentScale != goalSlow)
        {
            timeController.currentScale = Mathf.MoveTowards(timeController.currentScale, goalSlow, Time.unscaledDeltaTime * speed);
            timeController.fixedTime = Mathf.MoveTowards(timeController.fixedTime, .02f, Time.unscaledDeltaTime * speed);
            timeController.ChangeTimeScale();
            playerMovement.moveSpeed(speedType);
            Debug.Log("test");
        }

        

        if (Input.GetKey(KeyCode.S))
        {
            playerMovement.isCrouching = true;
        }
        else
        {
            playerMovement.isCrouching = false;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            physJump.Jump();
        }

        if (physJump.isJumping)
        {
            buttonPressTime += Time.unscaledDeltaTime;

            physJump.gravityChange(buttonPressTime, buttonPressWindow);
        }

    }
}
