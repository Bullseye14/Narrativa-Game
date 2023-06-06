using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    public CharacterController characterController;

    public IH_v2 inputHandler;

    Vector2 mouseDelta;
    float rotY;
    float rotX;

    float distanceFromTarget;
    float sensitivity;

    public MissionsManager manager;

    //// Start is called before the first frame update
    void Start()
    {
        sensitivity = 0.1f;
        distanceFromTarget = 10;
        target = characterController.transform;

        manager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
    }

    //// Update is called once per frame
    void Update()
    {
        if(manager.playerCanMove)
        {
            mouseDelta = inputHandler.mouseDelta * sensitivity;

            rotY += mouseDelta.x;
            rotX += -mouseDelta.y;

            //rotX *= sensitivity;
            //rotY *= sensitivity;

            rotX = Mathf.Clamp(rotX, 0, 60);

            transform.localEulerAngles = new Vector3(rotX, rotY, 0);

            transform.position = target.position - transform.forward * distanceFromTarget;
        }
    }


    private void OnEnable()
    {
       
       
    }

    private void OnDisable()
    {
       
    }

}





