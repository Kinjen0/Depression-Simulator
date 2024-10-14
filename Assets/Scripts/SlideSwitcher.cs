using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will let me select which slides to change, and when
/// </summary>
public class SlideSwitcher : MonoBehaviour
{
    public List<Slide> slides;
    int curentSlide = 0;

    public void Start()
    {
        foreach (var slide in slides)
        {
            // disable all slides
            slide.DisableSlide();
        }
        // Activate the first
        slides[0].EnableSlide();
    }

    public void NextSlide()
    {
        if (curentSlide < slides.Count - 1)
        {
            SetSlide(curentSlide+1);
        }
    }
    public void PreviousSlide()
    {
        if (curentSlide > 0)
        {
            SetSlide(curentSlide - 1);
        }
    }
    public void SetSlide(int index)
    {
        slides[curentSlide].DisableSlide();

        slides[index].EnableSlide();

        curentSlide = index;
    }

}
