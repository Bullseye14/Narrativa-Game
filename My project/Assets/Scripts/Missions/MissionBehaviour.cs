using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBehaviour : MonoBehaviour
{
    // 0 si fàcil, 1 si difícil
    public int dificultat;

    private string path = "Assets/Mission Manager/UI/Dialogs/Texts/";

    // Interactable hauria d'estar sempre?
    public bool interactable = true;

    // 0 for mission not active, 1 for mission active, 2 for post-mission
    public int missionState = 0;

    // Bool que ens diu si la missió està activa ara mateix
    public bool activeMission = false;

    public string DesiredFilePath()
    {
        string filePath = "";

        switch (missionState)
        {
            case 0:
                filePath = PreMission();
                break;

            case 1:
                filePath = Mission();
                break;

            case 2: // de moment postmission1, ja veurem com decidim si diu 1 o l'altre
                filePath = PostMission1();
                break;

        }

        return filePath;
    }

    private string PreMission()
    {
        return "PreMission/" + this.gameObject.name + ".txt";
    }

    private string Mission()
    {
        return "Mission/" + this.gameObject.name + ".txt";
    }

    private string PostMission1()
    {
        return "Post1/" + this.gameObject.name + ".txt";
    }

    private string PostMission2()
    {
        return "Post2/" + this.gameObject.name + ".txt";
    }

    private void OnTriggerEnter(Collider other)
    {
        interactable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        interactable = false;
    }
}
