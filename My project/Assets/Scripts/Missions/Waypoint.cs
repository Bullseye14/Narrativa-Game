using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Image img;
    public GameObject target;

    public MissionsManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GetTarget();

        if (target != null)
        {
            img.enabled = true;
            img.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
        }

        else img.enabled = false;
    }

    public GameObject GetTarget()
    {
        return manager.activeMission;
    }
}
