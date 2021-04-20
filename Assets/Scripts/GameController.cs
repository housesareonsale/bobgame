using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject badMan;
    public GameObject sprayMan;
    public PlayerControl playerControl;
    public TextMeshProUGUI floor;
    public GameState gameState;
    public ScreenShake screenShake;
    public LayerMask enemySpawningLayerMask;

    int maxNumEnemies;
    int gameFloor;
    int currNumEnemies;

    Transform finalRoomPosition;

    void Start()
    {
        gameState.gameController = gameObject.GetComponent<GameController>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        floor.text = "Floor " + gameState.gameFloor.ToString();
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;

        // Update player here 
        playerControl.playerWeapon.damage += gameState.attackIncreased;
        playerControl.fireRate += gameState.firerateIncreased;
        playerControl.player.maxHealth += gameState.healthIncreased;
        playerControl.playerWeapon.UpgradeBulk(gameState.playerWeaponUpgrades);
        if(gameState.currHealth != 0)
        {
            playerControl.player.health = gameState.currHealth;
        }

        Invoke("UpdateGraph", 5f);
        InvokeRepeating("SpawnEnemiesConstant", 7f, 2f);
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
        SpawnEnemies(1);
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

        for(int i = 0; i < enemiesSpawned; i++) 
        {

            Vector3 spawnLocation = Util.GetRandomPosition(playerControl.transform.position, startRange, endRange);
            Collider2D hitCollider = Physics2D.OverlapCircle(spawnLocation, 1, enemySpawningLayerMask);
            
            while(hitCollider != null)
            {
                spawnLocation = Util.GetRandomPosition(playerControl.transform.position, startRange, endRange);
                hitCollider = Physics2D.OverlapCircle(spawnLocation, 1, enemySpawningLayerMask);
            }

            int rand = Random.Range(0, 4);
            // 20% chance to spawn a skull girl
            // 80% chance to spawn a rocky monster 
            GameObject enemy =  rand == 0 ? 
                Instantiate(sprayMan, spawnLocation, Quaternion.identity) : 
                Instantiate(badMan, spawnLocation, Quaternion.identity);

            // half the size of the enemies because they use sprite stiching which make them appear bigger
            // scaling is also done in EnemyMovement at the end of FixedUpdate so make sure to change that if
            // any changes to scale have to be made
            enemy.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

            Enemy enemyObj = enemy.GetComponent<Enemy>();
            if(enemyObj != null)
            {
                float originalTotalVal = (float)enemyObj.health + (float)enemyObj.enemyDamage - enemyObj.firerate; 
                float increasedTotalVal = 
                    (float)gameState.enemyHealthIncrease + 
                    (float)gameState.enemyDamageIncrease - 
                    gameState.enemyFirerateIncrease;

                enemyObj.UpdateCurrencyDrop( increasedTotalVal/originalTotalVal );
                enemyObj.screenShake = screenShake;
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

    public void UpgradePlayerAttack()
    {
        playerControl.playerWeapon.damage += 7;
    }

    public void UpgradePlayerFireRate()
    {
        playerControl.fireRate += 0.08f;
    }

    public void UpgradePlayerHealh(int currIncrease, int maxIncrease = 0)
    {
        playerControl.UpdatePlayerHealth(currIncrease, maxIncrease);
    }
}
