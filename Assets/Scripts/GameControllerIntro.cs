using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerIntro : GameController
{
    public DialogueTrigger intro;
    public DialogueTrigger introContinued;
    public DialogueTrigger introFinal;
    public Enemy denverCol;
    public Elevator elevator;

    IntroDialogueState introDialogueState;

    void Start()
    {
        gameState.Initialize();
        gameState.gameController = gameObject.GetComponent<GameController>();
        gameState.playerLocation = playerControl.transform;
        gameFloor = gameState.gameFloor;
        floor.text = "Floor " + gameState.gameFloor.ToString();
        maxNumEnemies = gameState.maxNumEnemies;
        currNumEnemies = 0;
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>(); 

        StartIntro();
    }

    public override void CotinueEvent()
    {
        switch(introDialogueState)
        {
            case IntroDialogueState.INTRO:
                denverCol.cutScene = false;
                Invoke("ContinueIntro", 0.05f);
                break;
            case IntroDialogueState.INTRO_CONTINUED:
                playerControl.levelGenerationDone = true;
                introDialogueState = IntroDialogueState.OFF_DENVER_COL;
                break;
            case IntroDialogueState.OFF_DENVER_COL:
                ContinueIntroAfterDenver();
                break;
            case IntroDialogueState.INTRO_END:
                playerControl.levelGenerationDone = true;
                elevator.locked = false;
                break;
            default:
                break;
        }
    }

    void StartIntro()
    {
        elevator.locked = true;
        cutScene = true;
        intro.TriggerDialogue();
        introDialogueState = IntroDialogueState.INTRO;
        return;
    }

    void ContinueIntro()
    {
        audioController.PlayNormalMusic();
        denverCol.cutScene = true;
        introContinued.TriggerDialogue();
        introDialogueState = IntroDialogueState.INTRO_CONTINUED;
        return;
    }

    void ContinueIntroAfterDenver()
    {
        playerControl.levelGenerationDone = false;
        introFinal.TriggerDialogue();
        introDialogueState = IntroDialogueState.INTRO_END;
    }
}
