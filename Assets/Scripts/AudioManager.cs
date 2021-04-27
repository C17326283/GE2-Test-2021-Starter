using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //for managing and playing the random audio clips
    public AudioClip[] audioClips;
    public AudioSource audioSource;
   
    public void PlayBark()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.pitch = Random.Range(0.9f, 1.2f);
        audioSource.Play();
    }
}
