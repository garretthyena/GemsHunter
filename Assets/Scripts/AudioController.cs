using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip BirdsSFX;
    public AudioClip Steps_GRASS_SFX;
    public bool isWalking;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       audioSource.clip = BirdsSFX;
       audioSource.Play(); 
       isWalking = true;      
    }

    public void AudioSteps()
    { 
        audioSource.PlayOneShot(Steps_GRASS_SFX, 1f);
        StartCoroutine(Steps());
    }

    IEnumerator Steps()
    {
        yield return new WaitForSeconds(0.10f);
        if(isWalking == true)
        {
            AudioSteps();
            yield return StartCoroutine("Steps");
        }
    }

    public void Update()
    {
        if(isWalking == true)
        {
            Debug.Log("HAURIES DE SONAR");
            AudioSteps();
        }
    }
}