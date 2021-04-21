using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerElevator : GameController
{
    public GameObject textPopupComponent;
    public Elevator elevator;
    public TextMeshProUGUI timerText;
    public Transform[] spawnPoints;
    
    float timer = 10f;
    bool textDisplay = false;
    void Start()
    {
        gameState.gameController = gameObject.GetComponent<GameController>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        floor.text = "Floor " + gameState.gameFloor.ToString();
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;
        cutScene = false;
        elevator.locked = true;
        elevator.exiting = true;

        // Update player here 
        playerControl.levelGenerationDone = true;
        playerControl.playerWeapon.damage += gameState.attackIncreased;
        playerControl.fireRate += gameState.firerateIncreased;
        playerControl.player.maxHealth += gameState.healthIncreased;
        playerControl.playerWeapon.UpgradeBulk(gameState.playerWeaponUpgrades);
        if(gameState.currHealth != 0)
        {
            playerControl.player.health = gameState.currHealth;
        }

        InvokeRepeating("SpawnForElevator", 1f, 0.3f);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString() + " SECS TO LAND";

        if(timer <= 0 && !textDisplay)
        {
            textDisplay = true;
            GameObject textPopup = Instantiate(textPopupComponent, elevator.transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            DamagePopup textPopupObj = textPopup.GetComponent<DamagePopup>();
            textPopupObj.SetUpUnlockedElevator();
            Destroy(textPopup, 4f);
            elevator.locked = false;
        }
    }

    void SpawnForElevator()
    {
        Vector3 spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        GameObject enemy =  Instantiate(wizMan, spawnLocation, Quaternion.identity);

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
