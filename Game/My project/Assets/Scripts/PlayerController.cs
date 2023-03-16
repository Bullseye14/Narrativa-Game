using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum States
{
    idle,
    run,
    jump
}

public class PlayerController : MonoBehaviour
{

    InputHandler inputHandler;
    AnimatorHandler animatorHandler;

    CharacterController characterController;
    Rigidbody rb;
    Animator animator;
    public Camera camera;



    States currentState;
    States previousState;

    Vector3 moveDirection;
    Vector3 rotateDirection;



    private float runSpeed = 6;
    public float movementSpeed;

    public float jumpForce = 10f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //camera              = GetComponent<Camera>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponent<AnimatorHandler>();
    }
    void Start()
    {
        changeState(States.idle);
    }
    void changeState(States state)
    {
        previousState = currentState;
        currentState = state;
    }
    void Update()
    {

        switch (currentState)
        {
            case States.idle:
                if (inputHandler.moveAmount > 0.1 || inputHandler.moveAmount < -0.1) //existing movement
                {
                    changeState(States.run);
                }
                if (inputHandler.jumping == 1f)//jumping requested
                {
                    changeState(States.jump);
                }

                break;
            case States.run:
                if (inputHandler.moveAmount < 0.1 && inputHandler.moveAmount > -0.1)//non existing movement
                {
                    changeState(States.idle);
                }
                if (inputHandler.jumping == 1f)//jumping requested
                {
                    changeState(States.jump);
                }

                movementSpeed = runSpeed;

                break;
            case States.jump:
                if (characterController.isGrounded == true)//jumping end when touch ground
                {
                    changeState(previousState);
                }

                break;
        }
    }

    private void FixedUpdate()
    {
        if(currentState == States.jump)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
            inputHandler.jumping = 0f;
            
        }

        if(currentState == States.run || currentState == States.jump) //jumping or running
        {
            moveChC();
            rotateChC();
        }
        

    }

    public void moveChC()
    {
        if (inputHandler.verticalInput > 0.1)
        {
            characterController.Move(transform.forward * movementSpeed * Time.deltaTime);
        }
        else if (inputHandler.verticalInput < -0.1)
        {
            characterController.Move(transform.forward * -movementSpeed * Time.deltaTime);
        }
    }

    public void rotateChC()
    {
        if (inputHandler.horizontalInput > 0.1)
        {
            transform.Rotate(new Vector3(0f, 5f, 0f), Space.Self);
        }
        else if (inputHandler.horizontalInput < -0.1)
        {
            transform.Rotate(new Vector3(0f, -5f, 0f), Space.Self);
        }
    }

    private void OnGUI()
    {
        //string lookDirStr = "x = "+lookDir.x+" y = "+ lookDir.y;
        //GUILayout.Label(lookDirStr);
    }



}
