using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask roomLayer;
    public LevelGeneration levelGeneration;

    void Update()
    {
        if(levelGeneration.stopGeneration)
        {
            Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);
            if(roomDetection == null)
            {
                GameObject[] rooms = levelGeneration.rooms;
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
