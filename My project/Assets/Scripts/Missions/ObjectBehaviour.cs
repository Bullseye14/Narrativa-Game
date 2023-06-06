using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public MissionsManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        manager.activeMission.GetComponent<MissionBehaviour>().objectInteractable = this.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        manager.activeMission.GetComponent<MissionBehaviour>().objectInteractable = null;
    }
}
