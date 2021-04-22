using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameControllerBenDover : GameController
{
    public Elevator elevator;
    public GameObject benDover;
    public Enemy benDoverEnemy;
    public Weapon benDoverWeapon;
    public GameObject textPopupComponent;
    public DialogueTrigger benDoverIntro;
    public DialogueTrigger benDoverContinued;
    public DialogueTrigger benDoverEnd;

    BenDoverDialogState benDoverDialogState;

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

        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>(); 

        Invoke("StartBenInteraction", 5f);        
    }

    public override void CotinueEvent()
    {
        switch(benDoverDialogState)
        {
            case BenDoverDialogState.INTRO:
                Invoke("ContinueBenInteraction", 0.05f);
                break;
            case BenDoverDialogState.INTRO_CONTINUED:
                audioController.PlayNormalMusicHighTempo();
                playerControl.levelGenerationDone = true;
                benDoverEnemy.cutScene = false;
                benDoverDialogState = BenDoverDialogState.BOSS_FIGHT;
                break;
            case BenDoverDialogState.BOSS_FIGHT:
                HalfWayThere();
                break;
            case BenDoverDialogState.HALF_WAY:
                EndBenDoverScene();
                break;
            default:
                break;
        }
    }

    void StartBenInteraction()
    {
        playerControl.levelGenerationDone = false;
        benDoverIntro.TriggerDialogue(); 
        benDoverDialogState = BenDoverDialogState.INTRO;
    }

    void ContinueBenInteraction()
    {
        SpawnBoss();
        benDoverContinued.TriggerDialogue(); 
        benDoverDialogState = BenDoverDialogState.INTRO_CONTINUED;
    }

    void HalfWayThere()
    {
        audioController.PlaySadMusic();
        GameObject textPopup = Instantiate(textPopupComponent, playerControl.transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        DamagePopup textPopupObj = textPopup.GetComponent<DamagePopup>();
        textPopupObj.SetUpBenDover();
        Destroy(textPopup, 4f);

        if(benDoverWeapon != null)
        {
            benDoverWeapon.numProjectiles *= 2;
            benDoverWeapon.sizeIncrease += 1.2f;
        }

        benDoverDialogState = BenDoverDialogState.HALF_WAY;
    }

    void EndBenDoverScene()
    {
        benDoverEnd.TriggerDialogue(); 
        benDoverDialogState = BenDoverDialogState.FIGHT_END;
        elevator.locked = false;
    }

    void SpawnBoss()
    {
        Vector3 spawnLocation = new Vector3(
        playerControl.transform.position.x + 2f, 
        playerControl.transform.position.y, 
        playerControl.transform.position.z);

        Collider2D hitCollider = Physics2D.OverlapCircle(spawnLocation, 1, enemySpawningLayerMask);
        
        float tryThis = 2f;
        float tryThisToo = 1f;

        while(hitCollider != null)
        {
            spawnLocation = new Vector3(
            playerControl.transform.position.x - tryThis, 
            playerControl.transform.position.y + tryThisToo, 
            playerControl.transform.position.z);

            tryThis -= 0.5f;
            tryThisToo *= -1;
            hitCollider = Physics2D.OverlapCircle(spawnLocation, 1, enemySpawningLayerMask);
        }

        GameObject enemy = Instantiate(benDover, spawnLocation, Quaternion.identity);

        // half the size of the enemies because they use sprite stiching which make them appear bigger
        // scaling is also done in EnemyMovement at the end of FixedUpdate so make sure to change that if
        // any changes to scale have to be made
        enemy.transform.localScale = new Vector3(1f, 1f, 1f);

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
            enemyObj.enemyMovement.enemyScale = 1f;
            enemyObj.cutScene = true;
            enemyObj.deathEvent = deathEvent;
            benDoverEnemy = enemyObj;
        }

        Weapon enemyWeaponObj = enemy.GetComponent<Weapon>();
        if(enemyWeaponObj != null)
        {
            benDoverWeapon = enemyWeaponObj;
        }
    }
}
