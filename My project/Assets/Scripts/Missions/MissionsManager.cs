using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissionsManager : MonoBehaviour
{
    public int moralDecisions = 0;
    public int moiraDecisions = 0;

    // Llista amb totes les possibles missions
    public List<GameObject> missions;

    public List<GameObject> playableMissions;

    public GameObject missionsUI;

    public GameObject interactingMission;

    public InputAction inputActivateMission;

    public bool playerCanMove = true;

    public List<int> chosenNumbers;

    public GameObject decisionManager;

    // Start is called before the first frame update
    void Start()
    {
        RandomChosenMissions();

        NewActiveMission(chosenNumbers.Count);
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

        for (int i = 0; i < chosenNumbers.Count; ++i)
        {
            playableMissions[i].GetComponent<MissionBehaviour>().missionState = 0;
        }
    }

    private void NewActiveMission(int length)
    {
        int index = Random.Range(0, length);

        playableMissions[index].GetComponent<MissionBehaviour>().missionState = 1;

        chosenNumbers.RemoveAt(index);
    }

    public void SelectedAnswer(int value)
    {
        playerCanMove = true;

        // State 2 --> Missi� feta
        interactingMission.GetComponent<MissionBehaviour>().missionState = 2;

        // Dificultat de la missi�
        int diff = interactingMission.GetComponent<MissionBehaviour>().dificultat;

        // Si hem triat la decisi� esquerra
        if (value == 0)
        {
            // I era f�cil
            if(diff == 0)
            {
                moralDecisions += 1;
                moiraDecisions += 1;
            }

            // I era dif�cil
            else
            {
                moiraDecisions += 1;
            }

            // Hem triat opci� esquerra
            interactingMission.GetComponent<MissionBehaviour>().chosen = 0;
        }

        // Si hem triat la decisi� dreta
        else
        {
            // I era dif�cil
            if (diff == 1)
            {
                moralDecisions += 1;
            }

            // Hem triat opci� dreta
            interactingMission.GetComponent<MissionBehaviour>().chosen = 1;
        }

        NewActiveMission(chosenNumbers.Count);
    }

    private void Update()
    {
        if (interactingMission != null && inputActivateMission.ReadValue<float>() > 0.3 && playerCanMove)
        {
            playerCanMove = false;
            ActivateUI();
        }
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
