using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Rotate();

        transform.position = Vector3.Lerp(transform.position, (player.transform.position + offset), .25f);
        transform.LookAt(player.transform.position);
    }

    void Rotate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 2f, Vector3.up) * offset;
    }
}
