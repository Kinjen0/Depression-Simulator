using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=aUi9aijvpgs&t=74s

[System.Serializable]
public class GameData
{
    // Curent Level
    public int savedlevel;

    public SerializableDictionary<string, bool> taskCompletion;
    public GameData()
    {
        this.savedlevel = 0;
        this.taskCompletion = new SerializableDictionary<string, bool>();
    }
}
