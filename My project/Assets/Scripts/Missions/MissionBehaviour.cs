using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBehaviour : MonoBehaviour
{
    // 0 si fàcil, 1 si difícil
    public int dificultat;

    private string path = "Assets/Mission Manager/UI/Dialogs/Texts/";

    // 0 for mission not active, 1 for mission active, 2 for post-mission
    public int missionState = 0;

    // Bool que ens diu si la missió està activa ara mateix
    public bool activeMission = false;

    // Si és 0, vol dir que hem escollit l'opció de les Moires, si és 1, l'altra
    public int chosen;

    // 0 si és decisió només, 1 si és buscar objectes, 2 si és lluita
    public int type;

    public MissionsManager missionManager;

    public string fixedName;

    public List<GameObject> spawnObjects;

    private void Start()
    {
        missionManager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();

        fixedName = FixName();
    }

    // Perque el path sino busca Mission7(Clone), treiem el (Clone) i ho troba bé
    private string FixName()
    {
        string newname = this.gameObject.name;

        char[] removeThis = { '(', ')', 'e', 'o', 'l', 'C', 'n' };

        return newname.TrimEnd(removeThis);
    }

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

            case 2:
                filePath = PostMission(chosen);
                break;

        }

        return filePath;
    }

    private string PreMission()
    {
        return path + "PreMission/" + fixedName + ".txt";
    }

    private string Mission()
    {
        return path + "Mission/" + fixedName + ".txt";
    }

    private string PostMission(int chosen)
    {
        if (chosen == 0)
            return path + "Post1/" + fixedName + ".txt";

        else
            return path + "Post2/" + fixedName + ".txt";
    }

    public void MissionTextFinished()
    {
        switch (type)
        {
            case 0:
                missionManager.DecisionTime();
                break;

            case 1:
                missionManager.DecisionTime();
                break;

            case 2:
                missionManager.DecisionTime();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        missionManager.interactingMission = this.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        missionManager.interactingMission = null;
    }
}
