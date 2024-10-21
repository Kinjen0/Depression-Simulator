using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///  This task represents a single task for each player to complete
/// </summary>

public class Task : MonoBehaviour, IDataSaveInterface
{
    public string taskName;
    public bool isCompleted = false;
    public UnityEvent onTaskCompleted;
    public string taskDescription;


    public void SaveData(ref GameData data)
    {
        if (data.taskCompletion.ContainsKey(taskName))
        {
            data.taskCompletion[taskName] = isCompleted;
        }
        else
        {
            data.taskCompletion.Add(taskName, isCompleted);
        }
        Debug.Log("Saving Task State " + taskName + isCompleted);
    }

    public void LoadData(GameData data)
    {
        data.taskCompletion.TryGetValue(taskName, out isCompleted);
    }


    public void CompleteTask()
    {
        if(!isCompleted)
        {
            isCompleted = true;
            onTaskCompleted.Invoke();
            Debug.Log(taskName + ": completed ");
        }
    }
    
}
