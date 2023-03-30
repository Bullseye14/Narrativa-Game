using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public InputAction inputAction_move;
    public InputAction inputAction_SpaceBar;

    public AnimatorHandler animatorHandler;

    public MissionsManager missionsManager;

    public Vector2 inputMoveDir;
    public float jumping;

    public float moveAmount;

    public float verticalInput;
    public float horizontalInput;

    // Start is called before the first frame update
    void Awake()
    {
        animatorHandler = GetComponent<AnimatorHandler>();

        jumping = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (missionsManager.playerCanMove)
        {
            inputMoveDir = inputAction_move.ReadValue<Vector2>();
            jumping = inputAction_SpaceBar.ReadValue<float>();
            verticalInput = inputMoveDir.y;
            horizontalInput = inputMoveDir.x;

            handleMovementInput();
        }

        else
        {
            inputMoveDir = Vector2.zero;
            jumping = verticalInput = horizontalInput = 0f;

            handleMovementInput();
        }
    }

    private void handleMovementInput()
    {
        moveAmount = inputMoveDir.magnitude;
        
        animatorHandler.updateAnimatorValues(horizontalInput, verticalInput);
    }

    private void OnEnable()
    {
        inputAction_move.Enable();
        inputAction_SpaceBar.Enable();
    }
    private void OnDisable()
    {
        inputAction_move.Disable();
        inputAction_SpaceBar.Disable();
    }

}
