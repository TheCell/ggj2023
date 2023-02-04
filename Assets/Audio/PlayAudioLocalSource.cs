using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioLocalSource : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayAudioClip(SingleAudioClip clip)
    {
        audioSource.clip = clip.GetAudioClip;
        audioSource.volume = clip.GetVolume;
        audioSource.Play();
    } 
    
}
