using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.InputSystem;

public class DecisionManager : MonoBehaviour
{
    public GameObject arrow;
    public float arrowPos;

    private GameObject text1img;
    private GameObject text2img;

    private TextMeshProUGUI decision1;
    private TextMeshProUGUI decision2;

    private string[] decisionsText;
    private string filePath;

    private bool rightHovered = false;
    private bool leftHovered = false;

    public InputAction enterButton;

    public GameObject decisionMang;
    public MissionsManager missionsManager;

    // Start is called before the first frame update
    void Start()
    {
        // TO DO: SEMPRE SORTIRÀ LA MISSION 1
        filePath = "Assets/UI/Decisions/Texts/" + "Mission 1" + ".txt";
        decisionsText = File.ReadAllLines(filePath);

        text1img = GameObject.Find("Option 1 img");
        text2img = GameObject.Find("Option 2 img");

        text1img.transform.localScale = Vector3.one;
        text2img.transform.localScale = Vector3.one;

        decision1 = GameObject.Find("Option 1 img/Option 1").GetComponent<TextMeshProUGUI>();
        decision2 = GameObject.Find("Option 2 img/Option 2").GetComponent<TextMeshProUGUI>();

        decision1.text = decisionsText[0];
        decision2.text = decisionsText[1];
    }

    // Update is called once per frame
    void Update()
    {
        arrowPos = arrow.transform.rotation.z;

        if (enterButton.ReadValue<float>() > 0.3 && rightHovered)
        {
            decisionMang.SetActive(false);
            missionsManager.NewMission();
        }

        else if (enterButton.ReadValue<float>() > 0.3 && leftHovered)
        {
            decisionMang.SetActive(false);
            missionsManager.NewMission();
        }

        SelectedAnswer(arrowPos);        
    }

    public void SelectedAnswer(float direction)
    {
        // Decisió de la dreta seleccionada
        if (direction < -0.1)
        {
            rightHovered = true;
            leftHovered = false;

            text1img.transform.localScale = Vector3.one;
            text2img.transform.localScale = Vector3.one * 1.2f;
        }

        // Decisió de l'esquerra seleccionada
        else if (direction > 0.1)
        {
            rightHovered = false;
            leftHovered = true;

            text1img.transform.localScale = Vector3.one * 1.2f;
            text2img.transform.localScale = Vector3.one;
        }

        // Fletxa al mig
        else
        {
            rightHovered = false;
            leftHovered = false;

            text1img.transform.localScale = Vector3.one;
            text2img.transform.localScale = Vector3.one;
        }
    }

    private void OnEnable()
    {
        enterButton.Enable();
    }

    private void OnDisable()
    {
        enterButton.Disable();
    }
}
