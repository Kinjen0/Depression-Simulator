using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

// This will be a simple script to handle the dropping of cans in the tutorial scene. 
public class TutorialCanDropScript : MonoBehaviour
{
    private XRGrabInteractable GrabInteractable;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private float timeSinceDrop = 0;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundEffect;
    private void Start()
    {
        GrabInteractable = GetComponent<XRGrabInteractable>();
        startingPosition = this.gameObject.transform.position;
        startingRotation = this.gameObject.transform.rotation;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // If the object is being grabbed. We can start counting. 
        if (GrabInteractable.isSelected)
        {
            timeSinceDrop += Time.deltaTime;
            if(timeSinceDrop > 5)
            {
                ForceDrop();
            }
            StopAllCoroutines();
        }
        else
        {
            timeSinceDrop = 0; 
        }
    }


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

        GrabInteractable.interactionManager.SelectCancel(GrabInteractable.firstInteractorSelecting, GrabInteractable);
        audioSource.PlayOneShot(soundEffect);
        ReturnToStart();
    }

    // Helper function to return the object to its starting position. 
    private void ReturnToStart()
    {
        // Return it to its origional location
        this.gameObject.transform.position = startingPosition;
        this.gameObject.transform.rotation = startingRotation;
    }

    public void StartReturnToTable(int waitTime)
    {
        StartCoroutine(ReturnToTable(waitTime));
    }

    private IEnumerator ReturnToTable(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        audioSource.PlayOneShot(soundEffect);
        ReturnToStart();
    }
}
