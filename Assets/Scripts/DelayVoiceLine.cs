using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayVoiceLine : MonoBehaviour
{


    public VoiceLinePlayer voiceLinePlayer;

    public float delay;

    public AudioClip voiceClip;

    public void Start()
    {
        StartCoroutine(DelayVoiceLineCoroutine());
    }

    public IEnumerator DelayVoiceLineCoroutine()
    {
        yield return new WaitForSeconds(delay);
        voiceLinePlayer.PlayVoiceLine(voiceClip);
    }
}
