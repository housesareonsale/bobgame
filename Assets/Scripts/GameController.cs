using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public GameObject badMan;
    public GameObject sprayMan;
    public GameObject wizMan;
    public GameObject hrMan;
    public PlayerControl playerControl;
    public TextMeshProUGUI floor;
    public GameState gameState;
    public ScreenShake screenShake;
    public LayerMask enemySpawningLayerMask;
    public bool cutScene = false;
    public int maxNumEnemies;
    public int gameFloor;
    public int currNumEnemies;
    public Transform finalRoomPosition;
    public DialogueTrigger firstFloorStart;
    public DialogueTrigger secondFloorStart;
    public AudioController audioController;

    [Header("Events")]
    [Space]
    public UnityEvent deathEvent;

    GameDialogState gameDialogState;


    void Start()
    {
        gameState.gameController = gameObject.GetComponent<GameController>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        floor.text = "Floor " + gameState.gameFloor.ToString();
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;
        cutScene = false;
        gameDialogState = GameDialogState.NONE;

        // Update player here 
        playerControl.playerWeapon.damage += gameState.attackIncreased;
        playerControl.fireRate += gameState.firerateIncreased;
        playerControl.player.maxHealth += gameState.healthIncreased;
        playerControl.playerWeapon.UpgradeBulk(gameState.playerWeaponUpgrades);
        if(gameState.currHealth != 0)
        {
            playerControl.player.health = gameState.currHealth;
        }

        if(gameFloor == gameState.maxGameFloor - 1)
        {
            StartFirstFloor();
        }

        if(gameFloor == gameState.maxGameFloor - 2)
        {
            StartSecondFloor();
        }

        Invoke("UpdateGraph", 5f);
        InvokeRepeating("SpawnEnemiesConstant", 7f, 2f);
    }

    #region LEVEL AND ENEMIES
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
        if(!cutScene)
        {
            SpawnEnemies(1);
        }
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

            SpawnEnemy(spawnLocation);
        }
    }

    public void SpawnEnemy(Vector3 spawnLocation)
    {
        EnemyType enemyType = gameState.GetEnemyToSpawn();
        GameObject enemy =  null;

        if(enemyType == EnemyType.SPRAY_MAN)
        {
            enemy =  Instantiate(sprayMan, spawnLocation, Quaternion.identity);
        }
        else if(enemyType == EnemyType.HR_MAN)
        {
            enemy =  Instantiate(hrMan, spawnLocation, Quaternion.identity);
        }
        else if(enemyType == EnemyType.WIZ_MAN)
        {
            enemy =  Instantiate(wizMan, spawnLocation, Quaternion.identity);
        }
        else
        {
            enemy =  Instantiate(badMan, spawnLocation, Quaternion.identity);
        }

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
            if(deathEvent != null)
            {
                enemyObj.deathEvent = deathEvent;
            }
        }
    }

    public void LevelGenerationComplete(GameObject finalRoom)
    {
        RoomType room = finalRoom.GetComponent<RoomType>();
        room.GenerateElevator();

        if(gameDialogState == GameDialogState.NONE)
        {
            playerControl.levelGenerationDone = true;
        }
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

    #endregion
    #region DIALOGUE
    public virtual void CotinueEvent()
    {
        switch(gameDialogState)
        {
            case GameDialogState.FIRST_FLOOR_START:
                playerControl.levelGenerationDone = true;
                gameDialogState = GameDialogState.FIRST_FLOOR_END;
                cutScene = false;
                break;
            case GameDialogState.SECOND_FLOOR_START:
                playerControl.levelGenerationDone = true;
                gameDialogState = GameDialogState.SECOND_FLOOR_END;
                cutScene = false;
                break;
            default:
            case GameDialogState.NONE:
                break;
        }
    }

    void StartFirstFloor()
    {
        cutScene = true;
        playerControl.levelGenerationDone = false;
        firstFloorStart.TriggerDialogue();
        gameDialogState = GameDialogState.FIRST_FLOOR_START;
    }

    void StartSecondFloor()
    {
        cutScene = true;
        playerControl.levelGenerationDone = false;
        secondFloorStart.TriggerDialogue();
        gameDialogState = GameDialogState.SECOND_FLOOR_START;
    }
    #endregion
}
