using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// This script allows me to manually control the color shifting down through the hallway, and at the end they will have a normal color view. 
/// Got a bit of help from this tutorial. 
/// https://www.youtube.com/watch?v=SeXdXvtunNY
/// Its on bloom, but it works for this. 
/// </summary>
public class HallwayColorChange : MonoBehaviour
{
    public Transform player;
    public Volume postProcessVolume;

    private ColorAdjustments colorAdjustments;
    private float startX = 0f;
    private float endX = 90f;
    private float farthestPlayer; 
    private float initialSaturation; 
    private float targetSaturation = 0f; 
    private bool doneChanging = false;

    // Color settings
    private Color initialColor;
    private Color targetColor = Color.white;

    void Start()
    {
        // Try to grab the color adjustments
        if (postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            // Set the initial value for saturation if needed
            initialSaturation = colorAdjustments.saturation.value;
            initialColor = colorAdjustments.colorFilter.value;
            farthestPlayer = player.transform.position.x;
        }
        else
        {
            Debug.LogError("Cannot find ColorAdjustments.");
        }
    }

    void Update()
    {
        // Only update if the player hasn't completed the transition
        if (!doneChanging && colorAdjustments != null)
        {
            float playerX = player.position.x;
            Debug.Log(playerX);
            // Make sure the Effect wont reverse if the player goes back a bit
            if (playerX >= farthestPlayer && playerX != 0) 
            {
                // Figure out how far along the player is
                float playerProgress = playerX / endX;
                Debug.Log(playerProgress);
                // Adjust the values slowly as the player crosses the hallway, Lerp functions are great. 
                colorAdjustments.saturation.value = Mathf.Lerp(initialSaturation, targetSaturation, playerProgress);
                colorAdjustments.colorFilter.value = Color.Lerp(initialColor, targetColor, playerProgress);

                farthestPlayer = player.transform.position.x;
                if (playerX >= endX)
                {
                    doneChanging = true;
                }
            }
        }
    }
}
