using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerFinalBoss : GameController
{
    public DialogueTrigger finalBossIntro;
    public DialogueTrigger finalBossContinued;
    public DialogueTrigger finalBossEnd;
    public Enemy finalBoss;

    FinalBossDialogueState finalBossDialogueState;

    void Start()
    {
        gameState.gameController = gameObject.GetComponent<GameController>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        floor.text = "Final Floor";
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;
        cutScene = false;
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>(); 

        // Update player here
        playerControl.playerWeapon.damage += gameState.attackIncreased;
        playerControl.fireRate += gameState.firerateIncreased;
        playerControl.player.maxHealth += gameState.healthIncreased;
        playerControl.playerWeapon.UpgradeBulk(gameState.playerWeaponUpgrades);
        if(gameState.currHealth != 0)
        {
            playerControl.player.health = gameState.currHealth;
        }

        StartFinalBoss();
    }

    public override void CotinueEvent()
    {
        switch(finalBossDialogueState)
        {
            default:
            case FinalBossDialogueState.INTRO:
                playerControl.levelGenerationDone = true;
                finalBossDialogueState = FinalBossDialogueState.INTRO_NEXT;
                break;
            case FinalBossDialogueState.INTRO_NEXT:
                ContinueFinalBoss();
                break;
            case FinalBossDialogueState.INTRO_CONTINUED:
                playerControl.levelGenerationDone = true;
                finalBoss.cutScene = false;
                audioController.PlayIntenseBoss();
                finalBossDialogueState = FinalBossDialogueState.BOSS_FIGHT;
                break;
            case FinalBossDialogueState.BOSS_FIGHT:
                EndFinalBoss();
                break;
        }
    }

    void StartFinalBoss()
    {
        playerControl.levelGenerationDone = false;
        finalBossIntro.TriggerDialogue();
        finalBossDialogueState = FinalBossDialogueState.INTRO;
    }

    void ContinueFinalBoss()
    {
        playerControl.levelGenerationDone = false;
        finalBossContinued.TriggerDialogue();
        finalBossDialogueState = FinalBossDialogueState.INTRO_CONTINUED;
    }

    void EndFinalBoss()
    {
        audioController.PlaySadMusic();
        finalBossEnd.TriggerDialogue();
        finalBossDialogueState = FinalBossDialogueState.BEAT_BOSS;
    }
}
