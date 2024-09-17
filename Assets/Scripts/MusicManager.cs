using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will represent a music manager for the system
/// In its curent form, it will persist throughout the game, and continue playing tracks as needed
/// </summary>
public class MusicManager : MonoBehaviour
{

    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public int curentSong; 

    // Boolean to let me auto play the tracks, I may want to not auto play at some point. 

    void Start()
    {
        // We do not want this destroyed between scene. 
        DontDestroyOnLoad(this);
        curentSong = 0;

        if(audioClips != null )
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }

    public void Update()
    {
        // If the audioSource is not playing, we want to move to the next song. 
        if(!audioSource.isPlaying)
        {
           int nextSong = (curentSong + 1) % audioClips.Length;
           PlaySong(nextSong);
           curentSong = nextSong;
        }
    }

    // This is a script that will let me play a specefic song at a specefic index. 
    public void PlaySong(int index)
    {
        // Throw an error if there is no clip for this index, i.e. the index is invalid. 
        if (index >= audioClips.Length || index < 0)
        {
            Debug.LogError("Invalid Song Index. Index: " + index);
            return;
        }
        curentSong = index;
        audioSource.clip = audioClips[curentSong];
        audioSource.Play();
    }

    

}
