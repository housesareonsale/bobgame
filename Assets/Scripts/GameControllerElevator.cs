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
        audioSource = gameObject.GetComponent<AudioSource>();
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
            playerControl.player.HandleHealthBar();
        }

        InvokeRepeating("SpawnForElevator", 1f, 0.3f);
    }

    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString() + " SECS TO LAND";
        }
        else if(timer < 0 && !textDisplay)
        {
            textDisplay = true;
            timerText.text = "HURRY UP! GET OUT!!";
            elevator.locked = false;
        }
    }

    void SpawnForElevator()
    {
        Vector3 spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        SpawnEnemy(spawnLocation);
    }
}
