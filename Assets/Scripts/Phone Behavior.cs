using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.Events;


/// <summary>
/// This script will handle the phones behavior.in particular dropping it
/// </summary>
public class PhoneBehavior : MonoBehaviour
{
    private XRGrabInteractable GrabInteractable;

    [SerializeField] private double holdTimer;
    private double timeSinceGrab;


    [SerializeField] private int timesToBePickedUp;
    private int timesPickedUp;

    // Time since the last drop, To prevent them from picking it up too early
    [SerializeField] private double timeToWaitAfterDrop;
    private double timeSinceDrop;

    // Potential Event to use to manage the task completion
    public UnityEvent PhoneDone; 

    public void Start()
    {
        GrabInteractable = GetComponent<XRGrabInteractable>();
        timeSinceGrab = 0;
        timesPickedUp = 1;
        timeSinceDrop = 0;
    }
    private void Update()
    {
        // First we check if it has been grabbed at all
        if (GrabInteractable.isSelected && timesPickedUp < timesToBePickedUp)
        {
            // If it is selected, then we can work on the timer
            timeSinceGrab += Time.deltaTime;
            if (timeSinceGrab > holdTimer * timesPickedUp)
            {
                ForceDrop(); // To be implemented. 
                timesPickedUp++;
                timeSinceGrab = 0;
            }
        }
        else
        {
            timeSinceGrab = 0;
        }
        if(!GrabInteractable.enabled)
        {
            timeSinceDrop += Time.deltaTime;
            if(timeSinceDrop > timeToWaitAfterDrop)
            {
                GrabInteractable.enabled = true;
                timeSinceDrop = 0;
            }
        }
        else
        {
            timeSinceDrop = 0;
        }
    }

    /// <summary>
    /// Function to force the object to be dropped
    /// </summary>
    private void ForceDrop()
    {
        // Use the select cancel funtion to make it stop selecting it, and cause it to drop
        // This is done to ensure that the "select cancled" event is called, so that I can play haptics on the dropped phone. 
        GrabInteractable.interactionManager.SelectCancel(GrabInteractable.firstInteractorSelecting,GrabInteractable);
        GrabInteractable.enabled = false;
    }

    
}
