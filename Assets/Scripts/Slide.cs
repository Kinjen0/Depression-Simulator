using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will represent a single slide, and hold a list of objects to enable and disable
public class Slide : MonoBehaviour
{
    public List<GameObject> elements;

    public void EnableSlide()
    {
        foreach (GameObject obj in elements)
        {
            obj.SetActive(true);
        }
    }
    public void DisableSlide()
    {
        foreach (GameObject obj in elements)
        {
            obj.SetActive(false);
        }
    }


}
