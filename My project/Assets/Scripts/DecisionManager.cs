using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecisionManager : MonoBehaviour
{
    public GameObject arrow;
    public float arrowPos;

    private GameObject text1img;
    private GameObject text2img;

    private TextMeshProUGUI decision1;
    private TextMeshProUGUI decision2;

    // Start is called before the first frame update
    void Start()
    {
        text1img = GameObject.Find("Option 1 img");
        text2img = GameObject.Find("Option 2 img");

        text1img.transform.localScale = Vector3.one;
        text2img.transform.localScale = Vector3.one;

        decision1 = GameObject.Find("Option 1 img/Option 1").GetComponent<TextMeshProUGUI>();
        decision2 = GameObject.Find("Option 2 img/Option 2").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        arrowPos = arrow.transform.rotation.z;

        SelectedAnswer(arrowPos);        
    }

    public void SelectedAnswer(float direction)
    {
        // Decisió de la dreta seleccionada
        if (direction < -0.1)
        {
            text1img.transform.localScale = Vector3.one;
            text2img.transform.localScale = Vector3.one * 1.2f;
        }

        // Decisió de l'esquerra seleccionada
        else if (direction > 0.1)
        {
            text1img.transform.localScale = Vector3.one * 1.2f;
            text2img.transform.localScale = Vector3.one;
        }

        // Fletxa al mig
        else
        {
            text1img.transform.localScale = Vector3.one;
            text2img.transform.localScale = Vector3.one;
        }
    }
}
