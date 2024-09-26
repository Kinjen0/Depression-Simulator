using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Transformers;


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

    public AudioSource dropCollisionSound;
    public bool canPlaySound;

    // So I need to also send haptic impulses to the controllers
    [SerializeField] XRBaseController leftController;
    [SerializeField] XRBaseController rightController;
    

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

    public void OnCollisionEnter(Collision collision)
    {
        if(canPlaySound && !collision.gameObject.CompareTag("Player"))
        {
            dropCollisionSound.Play();
            canPlaySound = false;
        }
    }

    /// <summary>
    /// Function to force the object to be dropped
    /// </summary>
    private void ForceDrop()
    {
        // Use the select cancel funtion to make it stop selecting it, and cause it to drop
        // This is done to ensure that the "select cancled" event is called, so that I can play haptics on the dropped phone. 
        /*
        if (GrabInteractable.firstInteractorSelecting is XRBaseController controller)
        {
            controller.SendHapticImpulse(.2f, .1f);
        }
        */
        // Theoretically this should get the controller thats selecting it, and play haptics on it. The documentation is all over the place lol
        XRBaseInputInteractor controller = GrabInteractable.firstInteractorSelecting as XRBaseInputInteractor;
        if (controller != null)
        {
            // it goes intensity, duration
            controller.SendHapticImpulse(.2f, .5f);
        }


        GrabInteractable.interactionManager.SelectCancel(GrabInteractable.firstInteractorSelecting,GrabInteractable);
        GrabInteractable.enabled = false;
        canPlaySound = true;
    }

    // First lets make a quick function to figure out which interactor



}
