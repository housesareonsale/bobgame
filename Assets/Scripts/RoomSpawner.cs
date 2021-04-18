using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public OpeningDirection openingDirection;
    public bool spawned = false;
    public float waitTime = 4f;

    RoomTemplates templates;
    int rand;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        // Invoke("Spawn", 0.1f);
        Invoke("DeleteSpawner", waitTime);
    }

    void DeleteSpawner()
    {
        Destroy(gameObject);
    }

    void Spawn()
    {
        if(!spawned)
        {
            if(openingDirection == OpeningDirection.DOWN)
            {
                if(transform.position.y >= Util.GetDownYBound())
                {
                    rand = Random.Range(0, templates.downRooms.Length);
                    Instantiate(
                        templates.downRooms[rand], 
                        new Vector3 (transform.position.x, transform.position.y, 0), 
                        templates.downRooms[rand].transform.rotation);
                }
            } 
            else if(openingDirection == OpeningDirection.UP)
            {
                if(transform.position.y <= Util.GetUpYBound())
                {
                    rand = Random.Range(0, templates.upRooms.Length);
                    Instantiate(
                        templates.upRooms[rand], 
                        new Vector3 (transform.position.x, transform.position.y, 0), 
                        templates.upRooms[rand].transform.rotation);
                }
            }
            else if(openingDirection == OpeningDirection.LEFT)
            {
                if(transform.position.x <= Util.GetLeftXBound())
                {
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(
                        templates.leftRooms[rand], 
                        new Vector3 (transform.position.x, transform.position.y, 0), 
                        templates.leftRooms[rand].transform.rotation);
                }
            }
            else if(openingDirection == OpeningDirection.RIGHT)
            {
                if(transform.position.x >= Util.GetRightXBound())
                {
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(
                        templates.rightRooms[rand], 
                        new Vector3 (transform.position.x, transform.position.y, 0), 
                        templates.rightRooms[rand].transform.rotation);
                }
            }

            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.CompareTag("SpawnPoint")) 
        // {
        //     RoomSpawner roomSpawner = other.gameObject.GetComponent<RoomSpawner>(); 
        //     if(roomSpawner && !roomSpawner.spawned && !spawned) 
        //     {
        //         Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
        //         Destroy(gameObject);
        //     }
            
        //     spawned = true;
        // }
    }
}
