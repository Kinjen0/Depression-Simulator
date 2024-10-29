using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayScript : MonoBehaviour
{
    // Si tge way this script will work, is we will be using the distance from the player to the door to handle it. 

    public Transform player;
    public Transform farDoor;
    public Transform closeDoor;
    public float playerStartingX;

    public float minimumDistance = 1f;
    public float maximumDistance = 1f;

    // Light Objects for the scene
    public Light directionalLight;
    public float startLightIntensity;
    public float endLightIntensity;

    public Light flashlight;
    public float startFlashlightIntensity;
    public float endFlashlightIntensity;



    private void Start()
    {
        // So we want to just initialize the distances, in case they are not perfectly set
        farDoor.position = new Vector3(player.position.x + minimumDistance, farDoor.position.y, farDoor.position.z);
        closeDoor.position = new Vector3(player.position.x - maximumDistance, closeDoor.position.y, closeDoor.position.z);
        startLightIntensity = directionalLight.intensity;
        startFlashlightIntensity = flashlight.intensity;
        playerStartingX = player.position.x;
    }

    private void Update()
    {
        //Debug.Log(farDoor.transform.position.x);
        if (farDoor.transform.position.x <= 100)
        {
            // Here we want to check the distance to each door, then we will update it as needed to ensure it stays the distance away at all times. 
            float distanceToFarDoor =  farDoor.position.x - player.position.x;
            if (distanceToFarDoor < minimumDistance)
            {
                farDoor.position = new Vector3(player.position.x + minimumDistance, farDoor.position.y, farDoor.position.z);
            }

            float distanceToCloseDoor =  player.position.x - closeDoor.position.x;

            if (distanceToCloseDoor > maximumDistance)
            {
                closeDoor.position = new Vector3(player.position.x - maximumDistance, closeDoor.position.y, closeDoor.position.z);
            }
        }
        // This can convert me to a progress,
        float playerProgress = (player.position.x / 100);

        //Debug.Log(playerProgress);
        directionalLight.intensity = Mathf.Lerp(startLightIntensity, endLightIntensity, playerProgress);
        
    }

}
