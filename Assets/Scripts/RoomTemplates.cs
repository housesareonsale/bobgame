using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] upRooms;
    public GameObject[] downRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
    public List<GameObject> rooms;

    public float waitTime;
    bool spawnedElevator = false;
    public GameObject elevator;

    void Update()
    {
        if(waitTime <= 0 && !spawnedElevator)
        {
            // TODO:
            // spawn the elevator in room
            // rooms.Count - 1 or any other random room.
            Debug.Log("Elevator has been made");
            spawnedElevator = true;    
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

}
