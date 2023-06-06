using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IH_v2 : MonoBehaviour
{

    private InputActions playerInputActions;
    private InputAction move;
    private InputAction attack;
    private InputAction dash;
    
    private InputAction mouseMovement;
    public Vector2 mouseDelta;

    public PC_v2 playerController;
    public AH_v2 animatorHandler;

    public MissionsManager missionsManager;
    float lastAttack1;
    private void Awake()
    {
        playerInputActions = new InputActions();
        animatorHandler = GetComponent<AH_v2>();
        missionsManager =  GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
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

        if (missionsManager.playerCanMove)
        {
            playerController.setMovementValues(movement);
        }

        mouseDelta = mouseMovement.ReadValue<Vector2>();

    }

    void callAttack1(InputAction.CallbackContext obj)
    {
        if (missionsManager.playerCanMove)
        {
            if (!animatorHandler.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")&&Time.time-lastAttack1 > 3f)
            {
                lastAttack1 = Time.time;
                playerController.setAttackValues("attackorder1", 30);
            }

        }
    }

    void callAttack(InputAction.CallbackContext obj)
    {
        if (missionsManager.playerCanMove)
        {
            if (!animatorHandler.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                playerController.setAttackValues("attackorder", 20);
            }



        }
    }

    void callDash(InputAction.CallbackContext obj)
    {
        if (missionsManager.playerCanMove)
        {
            playerController.DashNow();
        }
    }

    private void OnEnable()
    {
        move = playerInputActions.Player.Move;
        move.Enable();


        playerInputActions.Player.Attack1.performed += callAttack1;
        playerInputActions.Player.Attack1.Enable();
        playerInputActions.Player.Attack.performed += callAttack;
        playerInputActions.Player.Attack.Enable();
        playerInputActions.Player.Dash.performed += callDash;
        playerInputActions.Player.Dash.Enable();

        mouseMovement = playerInputActions.Player.cameraRotation;
        mouseMovement.Enable();

    }

    private void OnDisable()
    {
        move.Disable();
        playerInputActions.Player.Attack.Disable();
        playerInputActions.Player.Attack1.Disable();
        playerInputActions.Player.Dash.Disable();
        mouseMovement.Disable();
    }


}
