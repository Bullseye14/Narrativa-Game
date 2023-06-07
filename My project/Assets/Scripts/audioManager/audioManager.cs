using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{


    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip music;

    private void Awake()
    {
        
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.Play(0);
    }
    public void PlaySound(AudioClip clip)
    {
       
    }

    public void changeMasterVolume(float newVol)
    {
        AudioListener.volume = newVol;
    }

}
