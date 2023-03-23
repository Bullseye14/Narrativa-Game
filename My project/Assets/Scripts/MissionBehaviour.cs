using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBehaviour : MonoBehaviour
{
    public Material mainMat;
    public Material activeMat;

    public bool interactable;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material = activeMat;
        interactable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material = mainMat;
        interactable = false;
    }
}
