using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///  This task represents a single task for each player to complete
/// </summary>

public class Task : MonoBehaviour
{
    public string taskName;
    public bool isCompleted;
    public UnityEvent onTaskCompleted;
    public string taskDescription;

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
