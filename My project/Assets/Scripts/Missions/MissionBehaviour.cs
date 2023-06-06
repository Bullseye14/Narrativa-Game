using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBehaviour : MonoBehaviour
{
    // 0 si f�cil, 1 si dif�cil
    public int dificultat;

    private string path = "Assets/Mission Manager/UI/Dialogs/Texts/";

    // 0 for mission not active, 1 for mission active, 2 for post-mission
    public int missionState = 0;

    // Bool que ens diu si la missi� est� activa ara mateix
    public bool activeMission = false;

    // Si �s 0, vol dir que hem escollit l'opci� de les Moires, si �s 1, l'altra
    public int chosen;

    // 0 si �s decisi� nom�s, 1 si �s buscar objectes, 2 si �s lluita
    public int type;

    public MissionsManager missionManager;

    public string fixedName;

    public List<GameObject> spawnObjectsList;
    public List<GameObject> objectsManagerList;

    public GameObject objectInteractable;

    public bool finishedMission = false;

    private void Start()
    {
        missionManager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();

        fixedName = FixName();
    }

    private void Update()
    {
        if(objectsManagerList.Count == 0)
        {
            finishedMission = true;
        }
    }

    // Perque el path sino busca Mission7(Clone), treiem el (Clone) i ho troba b�
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

    public void MissionSelection(int mDecision)
    {
        finishedMission = false;

        if ((dificultat == 0 && mDecision == 0) || (dificultat == 1 && mDecision == 1))
        {
            switch (type)
            {
                case 0:
                    missionManager.MissionResult(mDecision, dificultat);
                    break;

                case 1:
                    SpawnObjects();
                    break;

                case 2:
                    SpawnObjects();
                    break;
            }
        }
        else missionManager.MissionResult(mDecision, dificultat);
    }

    public void SpawnObjects()
    {
        for (int i = 0; i < spawnObjectsList.Count; ++i)
        {
            GameObject obj = Instantiate(spawnObjectsList[i], this.gameObject.transform);
            objectsManagerList.Add(obj);
        }
    }

    public void PickObject()
    {
        for (int i = 0; i < objectsManagerList.Count; ++i)
        {
            if (objectInteractable.name == objectsManagerList[i].name)
            {
                objectsManagerList[i].SetActive(false);
                objectsManagerList.RemoveAt(i);
                Destroy(objectInteractable);
            }
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
