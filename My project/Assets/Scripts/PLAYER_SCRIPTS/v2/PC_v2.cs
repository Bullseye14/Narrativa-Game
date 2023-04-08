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
    public float rotationSpeed = 4f;

    AH_v2 animatorHandler;

    CharacterController characterController;
    Rigidbody rb;

    public bool attacking;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animatorHandler = GetComponent<AH_v2>();

        movementSpeed = 10f;
        attacking = false;
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
        Debug.Log("pc movement = " + moveVec);

        animatorHandler.setAnimatorMovementValues(moveVec.magnitude);
    }

    public void setAttackValues(string str)
    {
        animatorHandler.playOneTimeAnimation(str);
        attacking = true;
    }

    private void moveChC()
    {
        if (verticalInput > 0.1)
        {
            
            characterController.Move(transform.forward * movementSpeed * Time.deltaTime);
        }
        else if (verticalInput < -0.1)
        {
            characterController.Move(transform.forward * -movementSpeed * Time.deltaTime);
        }
    }

    private void rotateChC()
    {
        if (horizontalInput > 0.1)
        {
            transform.Rotate(new Vector3(0f, rotationSpeed, 0f), Space.Self);
        }
        else if (horizontalInput < -0.1)
        {
            transform.Rotate(new Vector3(0f, -rotationSpeed, 0f), Space.Self);
        }
    }

    

}
