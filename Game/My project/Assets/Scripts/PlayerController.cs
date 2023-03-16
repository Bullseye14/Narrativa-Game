using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum States
{
    idle,
    run
}

public class PlayerController : MonoBehaviour
{

    InputHandler inputHandler;
    AnimatorHandler animatorHandler;

    CharacterController characterController;
    Rigidbody           rb;
    Animator            animator;
    public Camera       camera;



    States currentState;


    Vector3 moveDirection;
    Vector3 rotateDirection;


    private float walkSpeed = 3;
    private float runSpeed = 6;
    public float movementSpeed;

    float angle;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb                  = GetComponent<Rigidbody>();
        animator            = GetComponent<Animator>();
        //camera              = GetComponent<Camera>();
        inputHandler        = GetComponent<InputHandler>();
        animatorHandler     = GetComponent<AnimatorHandler>();
    }
    void Start()
    {
        currentState = States.idle;
    }

    void Update()
    {
        if(inputHandler.moveAmount > 0.55f)
        {
            movementSpeed = runSpeed;
        }else if(inputHandler.moveAmount < 0.55f)
        {
            movementSpeed = walkSpeed;
        }
        
        changeStates();
  
        
    }

    private void FixedUpdate()
    {
        moveCC();
        rotateCC();

    }

    public void moveCC()
    {
        

     
        if(inputHandler.verticalInput > 0)
        {
            characterController.Move(transform.forward * movementSpeed * Time.deltaTime);
        }
          
        
       
    }

    public void rotateCC()
    {
        //float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        //if(targetAngle > 0)
        //{
        //    //angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothVelocity);
        //    //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //    transform.Rotate(transform.up, targetAngle*Time.deltaTime);
        //}
    }

    private void OnGUI()
    {
        //string lookDirStr = "x = "+lookDir.x+" y = "+ lookDir.y;
        //GUILayout.Label(lookDirStr);
    }

  


    void changeStates()
    {
        
    }

    
}
