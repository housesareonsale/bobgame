using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerTwins : GameController
{
    public Elevator elevator;
    void Start()
    {
        gameState.gameController = gameObject.GetComponent<GameController>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        floor.text = "Floor " + gameState.gameFloor.ToString();
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;
        cutScene = false;

        // Update player here 
        playerControl.playerWeapon.damage += gameState.attackIncreased;
        playerControl.fireRate += gameState.firerateIncreased;
        playerControl.player.maxHealth += gameState.healthIncreased;
        playerControl.playerWeapon.UpgradeBulk(gameState.playerWeaponUpgrades);
        if(gameState.currHealth != 0)
        {
            playerControl.player.health = gameState.currHealth;
        }

        StartTwinInteraction();        
    }

    void StartTwinInteraction()
    {
        elevator.locked = true;
        playerControl.levelGenerationDone = true; 
    }

}
