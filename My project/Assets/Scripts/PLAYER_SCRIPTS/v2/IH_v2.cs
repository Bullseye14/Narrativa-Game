using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IH_v2 : MonoBehaviour
{

    private InputActions playerInputActions;
    private InputAction move;
    private InputAction attack;
    public PC_v2 playerController;

    private void Awake()
    {
        playerInputActions = new InputActions();
    }


    void Start()
    {
        playerController = GetComponent<PC_v2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 movement = move.ReadValue<Vector2>();
        playerController.setMovementValues(movement);
            
       
    }

    void callJump(InputAction.CallbackContext obj)
    {
       
    }

    private void OnEnable()
    {
        move = playerInputActions.Player.Move;
        move.Enable();

        playerInputActions.Player.Attack.performed += callJump;
        playerInputActions.Player.Attack.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        playerInputActions.Player.Attack.Disable();
    }


}
