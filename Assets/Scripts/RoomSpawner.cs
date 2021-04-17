using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public OpeningDirection openingDirection;
    public bool spawned = false;

    RoomTemplates templates;
    int rand;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if(!spawned)
        {
            if(openingDirection == OpeningDirection.DOWN)
            {
                rand = Random.Range(0, templates.downRooms.Length);
                Instantiate(templates.downRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
            } 
            else if(openingDirection == OpeningDirection.UP)
            {
                rand = Random.Range(0, templates.upRooms.Length);
                Instantiate(templates.upRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
            }
            else if(openingDirection == OpeningDirection.LEFT)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
            }
            else if(openingDirection == OpeningDirection.RIGHT)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);            
            }

            spawned = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint") 
            && other.GetComponent<RoomSpawner>().spawned ) 
        {
            Destroy(gameObject);
        }
    }
}
