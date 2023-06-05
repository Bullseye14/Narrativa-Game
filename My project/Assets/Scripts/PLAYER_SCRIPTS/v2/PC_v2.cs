using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_v2 : MonoBehaviour
{
    // Start is called before the first frame update

    //INPUTS
    public float verticalInput;
    public float horizontalInput;

    public float movementSpeed;
    public float rotationSpeed;

    AH_v2 animatorHandler;

    CharacterController characterController;
    public Camera camera;
    Rigidbody rb;
    Vector3 movementForward;
    public bool attacking;

    float DashCallTime;
    float speedMultiplyer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        animatorHandler = GetComponent<AH_v2>();

        movementSpeed = 10f;
        rotationSpeed = 6f;
        attacking = false;
        DashCallTime = 0;
        speedMultiplyer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        moveChC();
        rotateChC();

        if (attacking)
        {
           //set colliders for attack
        }
    }


    public void setMovementValues(Vector2 moveVec)
    {
                 
        horizontalInput = moveVec.x;
        verticalInput = moveVec.y;
       
        if(verticalInput != 0)
        {
            animatorHandler.setAnimatorMovementValues(moveVec.magnitude);
        }
        else
        {
            animatorHandler.setAnimatorMovementValues(0);
        }
        
    }

    public void setAttackValues(string str)
    {
        animatorHandler.playOneTimeAnimation(str);
        attacking = true;
    }

    public void DashNow()
    {
        if(Time.time - DashCallTime > 2)
        {
            DashCallTime = Time.time;
            speedMultiplyer = 6.0f;
        }    
    }

    private void moveChC()
    {
        
        if (Time.time - DashCallTime > 0.2)//dash duration
            speedMultiplyer = 1.0f;//return to normal velocity
        float y = transform.position.y;
        movementForward = Vector3.Normalize(transform.position - camera.transform.position);
        

        if (verticalInput > 0.1)
        {

            Vector3 lookDir = camera.transform.forward;
            lookDir.y = transform.forward.y;
            
            transform.forward = lookDir;
            characterController.Move(transform.forward * movementSpeed * Time.deltaTime * speedMultiplyer);

        }
        else if (verticalInput < -0.1)
        {

            Vector3 lookDir = camera.transform.forward;
            lookDir.y = transform.forward.y;

            transform.forward = lookDir;

            characterController.Move(transform.forward * -movementSpeed * Time.deltaTime * speedMultiplyer);

        }
        if (horizontalInput > 0.1)
        {
            characterController.Move(camera.transform.right * movementSpeed * Time.deltaTime * speedMultiplyer);
        }
        else if (horizontalInput < -0.1)
        {
            characterController.Move(camera.transform.right * -movementSpeed * Time.deltaTime * speedMultiplyer);
        }
    }

    private void rotateChC()
    {
        
    }

    

}
