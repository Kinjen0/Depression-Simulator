using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class TrashTask : MonoBehaviour
{
    // This script will be placed inside a trash can, in which the player will be prompted to toss trash and other reffuse in order to make their living environment better

    public int trashCount = 0;

    public List<GameObject> trashObjects;

    public TaskList taskList;
    public int taskIndex;



    public void OnTriggerEnter(Collider other)
    {
        XRGrabInteractable interactable = other.gameObject.GetComponent<XRGrabInteractable>();
        if (other.gameObject.tag == "Trash" && interactable != null)
        {
            if (!interactable.isSelected)
            {
                StartCoroutine(DestroyTrash(other.gameObject));
                
            }
        }

    }

    // Script to destroy the trash object after 3 seconds, To prevent the trash can from being overfilled. 
    IEnumerator DestroyTrash(GameObject gameObject)
    {

        yield return new WaitForSeconds(3);
        if (trashCount >= trashObjects.Count - 1)
        {
            taskList.MarkTaskComplete(taskIndex);
        }
        Destroy(gameObject.gameObject);
        trashCount++;
    }
}
