using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script is basically just to let me make a bunch of reusable events for different circumstances. 
/// To be expanded as needed for different purposes. 
/// 
/// Currently, only accepts onTriggerEnter events. 
/// </summary>
public class UsefulEvents : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke();
    }

}
