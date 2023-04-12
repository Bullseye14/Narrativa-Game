using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBehaviour : MonoBehaviour
{
    // TO DO: SI ESTÀ INTERACTABLE, POSAR ALGUN FEEDBACK AL PERSONATGE

    public bool interactable;

    private void OnTriggerEnter(Collider other)
    {
        interactable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        interactable = false;
    }
}
