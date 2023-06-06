using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Image img;
    public Transform target;

    public MissionsManager manager;
    public GameObject player;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        img.enabled = false;
        manager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GetTarget();

        if (target != null && manager.playerCanMove && !manager.doingMission) UpdateTarget(target);

        else if (target != null && manager.doingMission && manager.activeMission.GetComponent<MissionBehaviour>().finishedMission) UpdateTarget(target);

        else img.enabled = false;
    }

    private void UpdateTarget(Transform obj)
    {
        img.enabled = true;

        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = this.gameObject.GetComponent<Camera>().WorldToScreenPoint(obj.position + offset);

        if(Vector3.Dot((obj.position - player.transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
                pos.x = maxX;

            else pos.x = minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.rectTransform.position = pos;
    }

    public Transform GetTarget()
    {
        if (manager.activeMission != null)
            return manager.activeMission.transform;

        else return null;
    }
}
