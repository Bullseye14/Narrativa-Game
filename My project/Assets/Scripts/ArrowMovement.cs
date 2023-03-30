using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{
    public InputAction inputMoveArrow;

    public float inputArrow;

    public float speed = 3f;

    public Quaternion newRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputArrow = inputMoveArrow.ReadValue<float>();

        if (inputArrow != 0)
            MoveArrow();
    }

    void MoveArrow()
    {
        newRot = Quaternion.identity;
        newRot.z = inputArrow * 45f;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRot, Time.deltaTime * speed);
    }

    private void OnEnable()
    {
        inputMoveArrow.Enable();
    }
    private void OnDisable()
    {
        inputMoveArrow.Disable();
    }
}
