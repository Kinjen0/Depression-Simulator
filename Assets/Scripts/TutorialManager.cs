using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  This will be a simple script to manage the tutorial scene, and it will make use of existing tasks to make my life a bit easier. 
/// </summary>
public class TutorialManager : MonoBehaviour
{
    [SerializeField] TaskList taskList;
    [SerializeField] TextMeshProUGUI UITextObject;
    [SerializeField] Task curentTask;
    //private int curentTaskIndex;
    // The discovery of actions, makes this so much different
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction turnAction;
    // A boolean to track if the object has been grabbed, this will be handled in the inspector, using the grabbable component on said object. 
    public bool hasGrabbedObject = false; 


    private void Start()
    {
        // To start off we need to setup our tasks.
        curentTask = taskList.getTask(0);
        UITextObject.text = curentTask.taskDescription;
        moveAction.Enable();
        moveAction.performed += ctx => onMove();
        turnAction.Enable();
        turnAction.performed += ctx => onTurn();

    }


    private void Update()
    {
        if (!taskList.getTask(2).isCompleted && hasGrabbedObject)
        {
            taskList.MarkTaskComplete(2);
        }
    }

    private void onMove()
    {
        var moveVal = moveAction.ReadValue<Vector2>();
        if (moveVal.x >= 0.2 || moveVal.x <= 0.2 || moveVal.y >= 0.2 || moveVal.y <= 0.2) // Mitigating potential issues with stick drift, and only counting large movements
        {
            taskList.MarkTaskComplete(0);
            Debug.Log("Player Has Moved");
        }
    }

    private void onTurn()
    {
        var turnVal = turnAction.ReadValue<Vector2>();
        if(turnVal.x >= 0.2 || turnVal.x <= 0.2) // Only using the X axis so that it will not count teleportation motions. 
        {
            taskList.MarkTaskComplete(1);
            Debug.Log("Player has Turned" + turnVal);
        }
    }

    public void grabObject()
    {
        taskList.MarkTaskComplete(2);
    }
}
