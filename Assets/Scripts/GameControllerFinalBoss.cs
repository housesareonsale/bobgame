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
    public GameObject gameExit;
    public Transform gameExitLocation;

    Vector3 exitLocation;
    FinalBossDialogueState finalBossDialogueState;

    void Start()
    {
        gameState.gameController = gameObject.GetComponent<GameController>();
        audioSource = gameObject.GetComponent<AudioSource>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        floor.text = "Final Floor";
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;
        cutScene = false;
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>(); 
        exitLocation = gameExitLocation.position;

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
            case FinalBossDialogueState.BEAT_BOSS:
                GetExit();
                break;
            case FinalBossDialogueState.FIGHT_END:
                gameState.WinGame();
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

    void GetExit()
    {
        GameObject exit = Instantiate(gameExit, exitLocation, Quaternion.identity);
        ColliderTrigger exitCollider = exit.GetComponent<ColliderTrigger>();
        exitCollider.gameController = gameState.gameController;
        finalBossDialogueState = FinalBossDialogueState.FIGHT_END;
    }
}
