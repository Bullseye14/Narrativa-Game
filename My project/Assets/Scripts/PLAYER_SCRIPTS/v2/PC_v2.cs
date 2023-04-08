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



    CharacterController characterController;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded == true)
        {
            Debug.Log("cc grounded");
        }
    }


    private void FixedUpdate()
    {
        moveChC();
        rotateChC();
    }


    public void setMovementValues(Vector2 moveVec)
    {
        horizontalInput = moveVec.x;
        verticalInput = moveVec.y;
        Debug.Log("pc movement = " + moveVec);
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
