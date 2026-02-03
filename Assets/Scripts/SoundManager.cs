using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource a;
    public AudioSource b;
    
    public void PlaySound(int index)
    {
        a.clip = sounds[index];
        a.Play();
    }

    public void PlayWhisperSound()
    {
        b.clip = sounds[5];
        b.Play();
    }

    public void StopWhisperSound()
    {
        b.Stop();
    }
}
