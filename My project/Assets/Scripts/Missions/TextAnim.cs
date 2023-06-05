using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.InputSystem;

public class TextAnim : MonoBehaviour
{
    public InputAction accelerateButton;
    public InputAction nextButton;

    // Text
    [SerializeField] TextMeshProUGUI _textMeshPro;

    // Velocitat a la que passen les lletres
    private float speed = 0.1f;

    // Array amb totes les frases
    private string[] stringArray;

    // Per passar a la següent frase
    private bool waitingForNext = false;

    // Per accelerar mentres cliquem un botó
    public bool accelerating = false;

    // Iterador de frases
    int i = 0;

    // Path on estan tots els diàlegs
    private string filePath;

    public MissionsManager missionsManager;

    // Start is called before the first frame update
    void Start()
    {
        GetNewText();
    }

    public void GetNewText()
    {
        filePath = missionsManager.interactingMission.GetComponent<MissionBehaviour>().DesiredFilePath();
        stringArray = File.ReadAllLines(filePath);

        EndCheck();
    }

    public void EndCheck()
    {
        waitingForNext = false;

        if (i <= stringArray.Length - 1)
        {
            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
        }

        else
        {
            i = 0;
            _textMeshPro.text = "";

            this.gameObject.SetActive(false);

            missionsManager.playerCanMove = true;

            if (missionsManager.interactingMission.GetComponent<MissionBehaviour>().missionState == 1)
            {
                missionsManager.DecisionTime();
            }
        }
    }

    private void Update()
    {
        if (waitingForNext)
        {
            if (nextButton.ReadValue<float>() > 0.3)
                EndCheck();
        }

        else
        {
            if (accelerateButton.ReadValue<float>() > 0.3)
                accelerating = true;

            else accelerating = false;
        }

        if (accelerating)
            speed = 0.02f;
        else
            speed = 0.1f;
    }

    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();

        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;

        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                waitingForNext = true;
                break;
            }

            counter += 1;

            yield return new WaitForSeconds(speed);
        }
    }

    private void OnEnable()
    {
        accelerateButton.Enable();
        nextButton.Enable();
    }

    private void OnDisable()
    {
        accelerateButton.Disable();
        nextButton.Disable();
    }
}

