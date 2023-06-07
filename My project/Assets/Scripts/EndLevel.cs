using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public GameObject image;
    public Image blackOutSquare;

    MissionsManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
        blackOutSquare.color = new Color(blackOutSquare.color.r, blackOutSquare.color.g, blackOutSquare.color.b, 0);
    }

    public void ActivateEnd()
    {
        image.SetActive(true);
        StartCoroutine(FadeBlackOutSquare());
    }

    public IEnumerator FadeBlackOutSquare(float fadeSpeed = 0.2f, bool fadeToBlack = true)
    {
        Color objectColor = blackOutSquare.color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquare.color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.color = objectColor;

                yield return null;
            }

            manager.EndScene();
        }
    }
}
