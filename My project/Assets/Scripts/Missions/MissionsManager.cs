using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissionsManager : MonoBehaviour
{
    public List<GameObject> missions;
    public GameObject missionsUI;

    public GameObject activeMission;

    public GameObject interactingMission;

    public InputAction inputActivateMission;

    public bool playerCanMove = true;

    public GameObject decisionManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < missions.Count; ++i)
        {
            missions[i].SetActive(false);
        }

        NewMission();
    }

    private void RandomChosenMissions()
    {

    }

    private void SpawnAllMissions()
    {

    }

    public void NewMission()
    {
        playerCanMove = true;

        if (missions.Count > 0)
        {
            missions[Random.Range(0, missions.Count)].SetActive(true);

            for (int i = 0; i < missions.Count; ++i)
            {
                if (missions[i].activeSelf)
                {
                    activeMission = missions[i];
                    break;
                }
            }
        }        
    }

    public void ClearMission()
    {
        // playerCanMove = true;

        decisionManager.SetActive(true);
        decisionManager.GetComponent<DecisionManager>().Start();

        for (int i = 0; i < missions.Count; ++i)
        {
            if (activeMission.name == missions[i].name)
                missions.RemoveAt(i);
        }
        activeMission.gameObject.SetActive(false);
        activeMission = null;
    }

    private void Update()
    {
        if (activeMission != null)
            if (activeMission.GetComponent<MissionBehaviour>().interactable && inputActivateMission.ReadValue<float>() > 0.3)
            {
                ActivateUI(activeMission.name);
            }
    }

    private void ActivateUI(string name)
    {
        playerCanMove = false;

        missionsUI.SetActive(true);
    }

    private void OnEnable()
    {
        inputActivateMission.Enable();
    }

    private void OnDisable()
    {
        inputActivateMission.Disable();
    }
}
