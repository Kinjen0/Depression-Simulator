using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLinePlayer : MonoBehaviour
{
    // Our audiosource
    public AudioSource audioSource;

    // Play a specefic voiceLine
    public void PlayVoiceLine(AudioClip voiceLine)
    {
        if(audioSource != null && voiceLine != null)
        {
            audioSource.clip = voiceLine;
            audioSource.Play();
        }
    }
}
