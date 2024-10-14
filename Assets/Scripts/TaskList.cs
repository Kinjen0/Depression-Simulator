using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskList : MonoBehaviour
{
    // list of all Tasks for this list
    public List<Task> tasks;
    // Task to be called when every task is completed
    public UnityEvent onAllTasksCompleted;
    public void CheckTasksCompleted()
    {
        foreach (Task task in tasks)
        {
            if (!task.isCompleted)
            {
                return;
            }
        }
        // If none of them are broken
        onAllTasksCompleted.Invoke();
        Debug.Log("All Tasks Completed");
    }
    public void MarkTaskComplete(int taskIndex)
    {
        // If its a valid index, this fixes so many issues
        if (taskIndex < tasks.Count && taskIndex >= 0)
        {
            tasks[taskIndex].CompleteTask();
            // Then we just check if they are all complete
            CheckTasksCompleted();
        }
    }    
}
