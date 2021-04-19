using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameController : MonoBehaviour
{
    public GameObject badMan;
    public PlayerControl playerControl;
    public GameState gameState;

    int maxNumEnemies;
    int gameFloor;
    int currNumEnemies;

    Transform finalRoomPosition;

    void Start()
    {
        Debug.Log("Game controller started with " + gameState.gameFloor.ToString());
        gameState.gameController = gameObject.GetComponent<GameController>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;

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
            enemiesSpawned = Random.Range(3,5);
        }
        else
        {
            enemiesSpawned = numEnemiesToSpawn;
        }

        if(currNumEnemies + enemiesSpawned >= maxNumEnemies)
        {
            enemiesSpawned = maxNumEnemies - currNumEnemies;
        }

        currNumEnemies += enemiesSpawned; 

        for(int i = 0; i < enemiesSpawned; i++) {
            Vector3 spawnLocation = Util.GetRandomPosition(playerControl.transform.position, startRange, endRange);

            // int rand = Random.Range(0, 4);
            // 20% chance to spawn a skull girl
            // 80% chance to spawn a rocky monster 
            GameObject enemy =  Instantiate(badMan, spawnLocation, Quaternion.identity);

            // half the size of the enemies because they use sprite stiching which make them appear bigger
            // scaling is also done in EnemyMovement at the end of FixedUpdate so make sure to change that if
            // any changes to scale have to be made
            enemy.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

            Enemy enemyObj = enemy.GetComponent<Enemy>();
            if(enemyObj != null)
            {
                enemyObj.targetPosition = playerControl.transform;
            }
        }
    }

    public void LevelGenerationComplete(GameObject finalRoom)
    {
        RoomType room = finalRoom.GetComponent<RoomType>();
        room.GenerateElevator();
        playerControl.levelGenerationDone = true;
    }

    public void EnemyDied()
    {
        // play enemy death music here
        currNumEnemies -= 1;
    }
}
