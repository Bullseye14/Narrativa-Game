using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{
    public InputAction inputMoveArrow;

    private float inputArrow;

    private Vector3 currentRot;

    public float maxRot;
    public float rotationSpeed;

    private void Start()
    {
        SetArrowToIni();
    }

    // Update is called once per frame
    void Update()
    {
        inputArrow = inputMoveArrow.ReadValue<float>();

        RotateArrow(inputArrow);
    }

    void RotateArrow(float direction)
    {
        currentRot.z -= direction * rotationSpeed / 10;

        currentRot.z = Mathf.Clamp(currentRot.z, 0, maxRot * 2);
        transform.localRotation = Quaternion.Euler(currentRot);
    }

    public void SetArrowToIni()
    {
        currentRot = Vector3.zero;
        currentRot.z = maxRot;

        transform.Rotate(currentRot);
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
