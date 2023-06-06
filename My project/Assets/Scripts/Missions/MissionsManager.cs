using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MissionsManager : MonoBehaviour
{

    // Llista amb totes les possibles missions
    public List<GameObject> missions;

    public GameObject countersGO;
    public TMP_Text fragments;
    //public TMP_Text moral;

    public GameObject decisionManager;

    public GameObject missionsUI;

    public GameObject interactingMission;
    public GameObject activeMission;

    public int moralDecisions = 0;
    public int moiraDecisions = 0;

    public bool playerCanMove = true;

    public InputAction inputActivateMission;

    public List<int> chosenNumbers;
    public List<int> activateMissions;
    public List<GameObject> playableMissions;

    public List<int> usedMissions;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        RandomChosenMissions();

        NewActiveMission(activateMissions.Count);
    }

    private void RandomChosenMissions()
    {
        int newNum = Random.Range(0, 4);

        chosenNumbers.Add(newNum);

        while(chosenNumbers.Contains(newNum))
        {
            newNum = Random.Range(0, 4);
        }

        chosenNumbers.Add(newNum);

        newNum = Random.Range(4, 10);

        chosenNumbers.Add(newNum);

        for (int i = 0; i < 3; ++i)
        {
            while(chosenNumbers.Contains(newNum))
            {
                newNum = Random.Range(4, 10);
            }

            chosenNumbers.Add(newNum);
        }

        SpawnAllMissions();
    }

    private void SpawnAllMissions()
    {
        for (int i = 0; i < chosenNumbers.Count; ++i)
        {
            GameObject newMission = Instantiate(missions[chosenNumbers[i]], this.gameObject.transform);
            playableMissions.Add(newMission);
        }

        for (int i = 0; i < playableMissions.Count; ++i)
        {
            playableMissions[i].GetComponent<MissionBehaviour>().missionState = 0;
            activateMissions.Add(i);
        }
    }

    private void NewActiveMission(int length)
    {
        if (usedMissions.Count < 6)
        {
            while (usedMissions.Contains(index))
            {
                index = Random.Range(0, length);
            }

            usedMissions.Add(index);
            playableMissions[index].GetComponent<MissionBehaviour>().missionState = 1;
            activeMission = playableMissions[index];

            //activateMissions.RemoveAt(index);
        }
    }

    public void SelectedAnswer(int value)
    {
        // State 2 --> Missió feta
        interactingMission.GetComponent<MissionBehaviour>().missionState = 2;

        // Dificultat de la missió
        int diff = interactingMission.GetComponent<MissionBehaviour>().dificultat;

        // Si hem triat la decisió esquerra
        if (value == 0)
        {
            // I era fàcil
            if(diff == 0)
            {
                moralDecisions += 1;
                moiraDecisions += 1;
            }

            // I era difícil
            else
            {
                moiraDecisions += 1;
            }

            // Hem triat opció esquerra
            interactingMission.GetComponent<MissionBehaviour>().chosen = 0;
        }

        // Si hem triat la decisió dreta
        else
        {
            // I era difícil
            if (diff == 1)
            {
                moralDecisions += 1;
            }

            // Hem triat opció dreta
            interactingMission.GetComponent<MissionBehaviour>().chosen = 1;
        }

        NewActiveMission(activateMissions.Count);
        playerCanMove = true;
    }

    private void Update()
    {
        if (interactingMission != null && inputActivateMission.ReadValue<float>() > 0.3 && playerCanMove)
        {
            playerCanMove = false;
            ActivateUI();
        }

        if (playerCanMove) countersGO.SetActive(true);
        else countersGO.SetActive(false);

        fragments.text = moiraDecisions.ToString();
        //moral.text = moralDecisions.ToString();
    }

    private void ActivateUI()
    {
        missionsUI.SetActive(true);
        missionsUI.GetComponent<TextAnim>().GetNewText();
    }

    public void DecisionTime()
    {
        playerCanMove = false;

        decisionManager.SetActive(true);
        decisionManager.GetComponent<DecisionManager>().Start();
        decisionManager.GetComponent<DecisionManager>().NewDecision();
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
