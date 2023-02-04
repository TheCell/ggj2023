using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioCollection", menuName = "ScriptableObjects/SOAudioCollection", order = 1)]
public class SOAudioCollection : ScriptableObject
{
    //[SerializeField]
    //private SingleAudioClip spawnAudio;

    //[SerializeField]
    //private SingleAudioClip idleAudio;

    //[SerializeField]
    //private SingleAudioClip attackAudio;

    //[SerializeField]
    //private SingleAudioClip despawnAudio;

    [SerializeField]
    private AudioClip spawnAudio2;

    [SerializeField]
    private AudioClip idleAudio2;

    [SerializeField]
    private AudioClip attackAudio2;

    [SerializeField]
    private AudioClip despawnAudio2;

    //public SingleAudioClip GetSpawnAudio { get => spawnAudio; }

    //public SingleAudioClip GetAttackAudio { get => attackAudio; }

    //public SingleAudioClip GetIdleAudio { get => idleAudio; }

    //public SingleAudioClip GetDespawnAudio { get => despawnAudio; }
    public AudioClip GetSpawnAudio { get => spawnAudio2; }

    public AudioClip GetAttackAudio { get => attackAudio2; }

    public AudioClip GetIdleAudio { get => idleAudio2; }

    public AudioClip GetDespawnAudio { get => despawnAudio2; }

}
