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

    public MissionsManager missionsManager;

    public GameObject F1;
    public GameObject F2;

    public void Start()
    {
        arrow.GetComponent<ArrowMovement>().SetArrowToIni();

        text1img = GameObject.Find("Text/Option 1 img");
        text2img = GameObject.Find("Text/Option 2 img");

        text1img.transform.localScale = Vector3.one;
        text2img.transform.localScale = Vector3.one;

        decision1 = GameObject.Find("Text/Option 1 img/Option 1").GetComponent<TextMeshProUGUI>();
        decision2 = GameObject.Find("Text/Option 2 img/Option 2").GetComponent<TextMeshProUGUI>();

        SelectedAnswer(0);
    }

    public void NewDecision()
    {
        arrow.GetComponent<ArrowMovement>().SetArrowToIni();

        filePath = "Assets/Mission Manager/UI/Decisions/Texts/" + missionsManager.interactingMission.GetComponent<MissionBehaviour>().fixedName + ".txt";

        decisionsText = File.ReadAllLines(filePath);

        decision1.text = decisionsText[0];
        decision2.text = decisionsText[1];
    }

    // Update is called once per frame
    void Update()
    {
        arrowPos = arrow.transform.rotation.z;

        if (enterButton.ReadValue<float>() > 0.3 && rightHovered)
        {
            this.gameObject.SetActive(false);
            missionsManager.SelectedAnswer(1);
        }

        else if (enterButton.ReadValue<float>() > 0.3 && leftHovered)
        {
            this.gameObject.SetActive(false);
            missionsManager.SelectedAnswer(0);
        }

        SelectedAnswer(arrowPos);        
    }

    public void SelectedAnswer(float direction)
    {
        // Decisió de la dreta seleccionada
        if (direction < -0.1)
        {
            F2.SetActive(true);
            F1.SetActive(false);

            rightHovered = true;
            leftHovered = false;

            text1img.transform.localScale = Vector3.one;
            text2img.transform.localScale = Vector3.one * 1.2f;
        }

        // Decisió de l'esquerra seleccionada
        else if (direction > 0.1)
        {
            F2.SetActive(false);
            F1.SetActive(true);

            rightHovered = false;
            leftHovered = true;

            text1img.transform.localScale = Vector3.one * 1.2f;
            text2img.transform.localScale = Vector3.one;
        }

        // Fletxa al mig
        else
        {
            F2.SetActive(false);
            F1.SetActive(false);

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
