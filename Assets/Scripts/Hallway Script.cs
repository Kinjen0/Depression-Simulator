using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayScript : MonoBehaviour
{
    // Si tge way this script will work, is we will be using the distance from the player to the door to handle it. 

    public Transform player;
    public Transform farDoor;
    public Transform closeDoor;

    public float minimumDistance = 1f;
    public float maximumDistance = 1f;

    private void Start()
    {
        // So we want to just initialize the distances, in case they are not perfectly set
        farDoor.position = new Vector3(player.position.x - minimumDistance, farDoor.position.y, farDoor.position.z);
        closeDoor.position = new Vector3(player.position.x + maximumDistance, closeDoor.position.y, closeDoor.position.z);

    }

    private void Update()
    {
        // Here we want to check the distance to each door, then we will update it as needed to ensure it stays the distance away at all times. 
        float distanceToFarDoor = player.position.x - farDoor.position.x;
        if (distanceToFarDoor < minimumDistance)
        {
            farDoor.position = new Vector3(player.position.x - minimumDistance, farDoor.position.y, farDoor.position.z);
        }
        
        float distanceToCloseDoor =  closeDoor.position.x - player.position.x;

        if (distanceToCloseDoor > maximumDistance)
        {
            closeDoor.position = new Vector3(player.position.x + maximumDistance, closeDoor.position.y, closeDoor.position.z);
        }
    }

}
