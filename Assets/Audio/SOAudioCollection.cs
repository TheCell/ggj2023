using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioCollection", menuName = "ScriptableObjects/SOAudioCollection", order = 1)]
public class SOAudioCollection : ScriptableObject
{
    //this originally used SIngleAudioClip class whith serialization which caused null pointer errors
    //ideally refactor into a struct so volume and audio clip can both be returned at same time

    [SerializeField]
    private AudioClip spawnAudio;

    [SerializeField]
    [Range(0f, 1f)]
    private float spawnVolume = 1f;

    [SerializeField]
    private AudioClip idleAudio;

    [SerializeField]
    [Range(0f, 1f)]
    private float idleVolume = 1f;

    [SerializeField]
    private AudioClip attackAudio;

    [SerializeField]
    [Range(0f, 1f)]
    private float attackVolume = 1f;

    [SerializeField]
    private AudioClip despawnAudio;

    [SerializeField]
    [Range(0f, 1f)]
    private float despawnVolume = 1f;

    public AudioClip GetSpawnAudio { get => spawnAudio; }
    public float GetSpawnVolume { get => spawnVolume; }

    public AudioClip GetAttackAudio { get => attackAudio; }
    public float GetAttackVolume { get => attackVolume; }

    public AudioClip GetIdleAudio { get => idleAudio; }
    public float GetIdleVolume { get => idleVolume; }


    public AudioClip GetDespawnAudio { get => despawnAudio; }
    public float GetDespawnVolume { get => despawnVolume; }

}
