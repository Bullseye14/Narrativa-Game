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
    private float speed = 0.05f;

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

    // Start is called before the first frame update
    void Start()
    {
        // IMPORTANT QUE ELS GAMEOBJECTS DE TEXTMESHPRO I EL .TXT ES DIGUIN IGUAL
        filePath = "Assets/UI/Dialogs/Texts/" + this.gameObject.name + ".txt";
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
    }

    private void Update()
    {
        if (waitingForNext)
        {
            // Això ara ho he hagut de canviar perquè no sé 
            // com funcionen els input actions aquests i ara mateix no va
            if (nextButton.triggered)
                EndCheck();
        }
        else
        {
            // Això ara ho he hagut de canviar perquè no sé 
            // com funcionen els input actions aquests i ara mateix no va
            if (accelerateButton.enabled)
                accelerating = true;

            else accelerating = false;
        }

        if (accelerating)
            speed = 0.02f;
        else
            speed = 0.05f;
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
}

