using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This script lets me attach voice Lines to triggers and play them
public class HallwayTriggers : MonoBehaviour
{
    bool hasPlayed = false;

    public AudioClip clip;
    public VoiceLinePlayer voiceLinePlayer;
    public UnityEvent reachedPoint;


    public void OnTriggerEnter(Collider other)
    {
        if(!hasPlayed)
        {
            voiceLinePlayer.PlayVoiceLine(clip);
            hasPlayed = true;
            reachedPoint.Invoke();
        }
    }
}
