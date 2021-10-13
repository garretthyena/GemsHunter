using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip BirdsSFX;
    public AudioClip Steps_GRASS_SFX;

    private AudioSource audioSource;

    void Awake()    
    {
        AudioSource audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
       audioSource.clip = BirdsSFX;
       audioSource.Play();       
    }

    public void AudioSteps()
    { 
        audioSource.PlayOneShot(Steps_GRASS_SFX, 1f);
    }
    
}