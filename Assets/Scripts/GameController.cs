using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameController : MonoBehaviour
{   
    // TODO: add random enemy generation script here
    // also anything to do with controlling the game
    public GameObject badMan;
    public Transform playerLocation;
    void Start()
    {
        Invoke("UpdateGraph", 5f);
        InvokeRepeating("SpawnEnemiesConstant", 10f, 10f);
    }

    void UpdateGraph()
    {
        // Reload the graph after 5 seconds of loading the screen
        // This is because the map is generated, so enemies try
        // to go through obstacles
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }

    void SpawnEnemiesConstant()
    {
        SpawnEnemies(5);
    }

    public void SpawnEnemies(int numEnemiesToSpawn = 0, float startRange = 8f, float endRange = 13f)
    {
        int enemiesSpawned = 0;
        if(numEnemiesToSpawn == 0)
        {
            // Add this to game state so there aren't infinite enemies
        }
        else
        {
            enemiesSpawned = numEnemiesToSpawn;
        }

        for(int i = 0; i < enemiesSpawned; i++) {
            Vector3 spawnLocation = Util.GetRandomPosition(playerLocation.position, startRange, endRange);

            // int rand = Random.Range(0, 4);
            // 20% chance to spawn a skull girl
            // 80% chance to spawn a rocky monster 
            GameObject enemy =  Instantiate(badMan, spawnLocation, Quaternion.identity);

            Enemy enemyObj = enemy.GetComponent<Enemy>();
            if(enemyObj != null)
            {
                enemyObj.targetPosition = playerLocation;
            }
        }
    }

}
