using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TaskList : MonoBehaviour
{
    // list of all Tasks for this list
    public List<Task> tasks;
    // Task to be called when every task is completed
    public UnityEvent onAllTasksCompleted;

    public TextMeshProUGUI TaskText;

    public void Start()
    {
        // Initialize the UI
        UpdateTaskListUI();
    }

    public Task getTask(int index)
    {
        return tasks[index];
    }

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

    public void UpdateTaskListUI()
    {
        string newText = "";
        for (int i = 0; i < tasks.Count; ++i)
        {
            if (tasks[i].isCompleted)
            {
                // Make use of color tags to make it more obvious that its done.
                newText += "<color=grey>" + tasks[i].taskDescription + " (Complete)" + "</color>";
            }
            else
            {
                newText += "<color=white>" + tasks[i].taskDescription + "</color>";
            }
            if(i < tasks.Count - 1)
            {
                newText += "<br>";
            }
        }

        TaskText.text = newText;

    }
    public void MarkTaskComplete(int taskIndex)
    {
        // If its a valid index, this fixes so many issues
        if (taskIndex < tasks.Count && taskIndex >= 0)
        {
            tasks[taskIndex].CompleteTask();
            UpdateTaskListUI();
            // Then we just check if they are all complete
            CheckTasksCompleted();
        }
    }    
}
