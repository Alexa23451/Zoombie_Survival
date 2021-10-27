using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource audioSource;

    public AudioClip shotSound;
    public AudioClip hitSound;

    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    //phát âm thanh
    public void PlayAudio(AudioClip a) => audioSource.PlayOneShot(a);
    
}
