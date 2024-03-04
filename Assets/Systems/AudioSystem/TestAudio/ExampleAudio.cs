using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExampleAudio : MonoBehaviour
{
    [SerializeField] private SimpleAudioEvent audioEvent; // Put here the scripatble object simpleAudioEvent 

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlaySound();
    }

    private void PlaySound()
    {
        audioEvent.Play(audioSource);
    }

}
