using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioCollection", menuName = "ScriptableObjects/SOAudioCollection", order = 1)]
public class SOAudioCollection : ScriptableObject
{
    [SerializeField]
    private SingleAudioClip spawnAudio;

    [SerializeField]
    private SingleAudioClip idleAudio;

    [SerializeField]
    private SingleAudioClip attackAudio;

    [SerializeField]
    private SingleAudioClip despawnAudio;
}
