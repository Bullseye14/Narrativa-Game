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
    public PC_v2 playerController;

    public MissionsManager missionsManager;
    private void Awake()
    {
        playerInputActions = new InputActions();
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
        
            
       
    }

    void callAttack1(InputAction.CallbackContext obj)
    {
        if (missionsManager.playerCanMove)
        {
            playerController.setAttackValues("attackorder1");
        }
    }

    void callAttack(InputAction.CallbackContext obj)
    {
        if (missionsManager.playerCanMove)
        {
            float attack = Random.Range(0f, 10f);
            if(attack < 7) {
                playerController.setAttackValues("attackorder");
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

        
    }

    private void OnDisable()
    {
        move.Disable();
        playerInputActions.Player.Attack.Disable();
        playerInputActions.Player.Attack1.Disable();
        playerInputActions.Player.Dash.Disable();
        
    }


}
