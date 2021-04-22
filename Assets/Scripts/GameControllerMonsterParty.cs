using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerMonsterParty : GameController
{
    public Elevator elevator;
    public int totalToSpawn;
    public DialogueTrigger monsterPartyIntro;
    public DialogueTrigger monsterPartyEnd;

    int numSpawned;
    int numKilled;
    MonsterPartyDialogState monsterPartyDialogState;

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
        numKilled = 0;

        maxNumEnemies = totalToSpawn;
        // Update player here
        playerControl.playerWeapon.damage += gameState.attackIncreased;
        playerControl.fireRate += gameState.firerateIncreased;
        playerControl.player.maxHealth += gameState.healthIncreased;
        playerControl.playerWeapon.UpgradeBulk(gameState.playerWeaponUpgrades);
        if(gameState.currHealth != 0)
        {
            playerControl.player.health = gameState.currHealth;
            playerControl.player.HandleHealthBar();
        }

        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>(); 
        StartMonsterParty();
    }

    public override void CotinueEvent()
    {
        switch(monsterPartyDialogState)
        {
            case MonsterPartyDialogState.INTRO:
                playerControl.levelGenerationDone = true;
                InvokeRepeating("SpawnForMonsterParty", 2f, 3f);
                audioController.PlayNormalMusicHighTempo();
                monsterPartyDialogState = MonsterPartyDialogState.BOSS_FIGHT;
                break;
            default:
            case MonsterPartyDialogState.BOSS_FIGHT:
                numKilled += 1;
                if(numKilled >= totalToSpawn)
                {
                    EndMonsterParty();
                }
                break;
            case MonsterPartyDialogState.BEAT_BOSS:
                monsterPartyDialogState = MonsterPartyDialogState.FIGHT_END;
                break;
        }
    }

    void SpawnForMonsterParty()
    {
        if(numSpawned < totalToSpawn)
        {
            numSpawned += 5;
            SpawnEnemies(5, 4f, 3f);
        }
        else
        {
            CancelInvoke("SpawnForMonsterParty");
        }
    }

    void StartMonsterParty()
    {
        playerControl.levelGenerationDone = false;
        monsterPartyIntro.TriggerDialogue();
        monsterPartyDialogState = MonsterPartyDialogState.INTRO;
    }
    
    void EndMonsterParty()
    {
        elevator.locked = false;
        monsterPartyEnd.TriggerDialogue();
        monsterPartyDialogState = MonsterPartyDialogState.BEAT_BOSS;
    }
}
