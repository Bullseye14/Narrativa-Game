using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScene : MonoBehaviour
{

    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Debug.Log("started");
    }



   

    
    void Update()
    {
        
    }
}
