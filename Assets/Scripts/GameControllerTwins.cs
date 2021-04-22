using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameControllerTwins : GameController
{
    public Elevator elevator;
    public GameObject HRGuyFred;
    public GameObject HRGuyJack;
    public GameObject textPopupComponent;
    public DialogueTrigger twinBossIntro;
    public DialogueTrigger twinBossContinued;
    public DialogueTrigger twinBossTwoKilled;
    public Enemy HRGuyFredObj;
    public Enemy HRGuyJackObj;
    public Weapon hRGuyFredWeaponObj;
    public Weapon hRGuyJackWeaponObj;

    TwinBossDialogState twinBossDialogState;

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
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>(); 

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

        Invoke("StartTwinInteraction", 5f);        
    }

    public override void CotinueEvent()
    {
        switch(twinBossDialogState)
        {
            case TwinBossDialogState.INTRO:
                Invoke("ContinueTwinInteraction", 0.05f);
                break;
            case TwinBossDialogState.INTRO_CONTINUED:
                playerControl.levelGenerationDone = true;
                HRGuyFredObj.cutScene = false;
                HRGuyJackObj.cutScene = false;
                twinBossDialogState = TwinBossDialogState.BOSS_FIGHT;
                break;
            case TwinBossDialogState.BOSS_FIGHT:
                OneTwinKilled();
                twinBossDialogState = TwinBossDialogState.ONE_DIED;
                break;
            case TwinBossDialogState.ONE_DIED:
                TwoTwinsKilled();
                break;
            case TwinBossDialogState.TWO_DIED:
                audioController.PlayNormalMusic();
                twinBossDialogState = TwinBossDialogState.FIGHT_END;
                break;
            default:
                break;
        }
    }

    void StartTwinInteraction()
    {
        playerControl.levelGenerationDone = false;
        twinBossIntro.TriggerDialogue(); 
        twinBossDialogState = TwinBossDialogState.INTRO;
    }

    void ContinueTwinInteraction()
    {
        SpawnBoss(true, 1f, 3f);
        SpawnBoss(false, 1f, 3f);
        twinBossContinued.TriggerDialogue(); 
        audioController.PlayTwinBossMusic();
        twinBossDialogState = TwinBossDialogState.INTRO_CONTINUED;
    }

    void OneTwinKilled()
    {
        GameObject textPopup = Instantiate(textPopupComponent, playerControl.transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        DamagePopup textPopupObj = textPopup.GetComponent<DamagePopup>();
        textPopupObj.SetUpTwinOneBeat();
        Destroy(textPopup, 8f);

        if(hRGuyFredWeaponObj != null)
        {
            hRGuyFredWeaponObj.numProjectiles += 4;
            hRGuyFredWeaponObj.fireEffect += 4;
            hRGuyFredWeaponObj.sizeIncrease += 0.75f;
        }

        if(hRGuyJackWeaponObj != null)
        {
            hRGuyJackWeaponObj.numProjectiles += 4;
            hRGuyJackWeaponObj.fireEffect += 4;
            hRGuyJackWeaponObj.sizeIncrease += 0.75f;
        }

        twinBossDialogState = TwinBossDialogState.ONE_DIED;
    }

    void TwoTwinsKilled()
    {
        twinBossTwoKilled.TriggerDialogue(); 
        elevator.locked = false;
        twinBossDialogState = TwinBossDialogState.TWO_DIED;
    }

    void SpawnBoss(bool whichOne, float startRange, float endRange)
    {
        Vector3 spawnLocation = new Vector3(
        playerControl.transform.position.x + 2f, 
        playerControl.transform.position.y, 
        playerControl.transform.position.z);

        if(whichOne)
        {
            spawnLocation = new Vector3(
            playerControl.transform.position.x, 
            playerControl.transform.position.y + 2f, 
            playerControl.transform.position.z);
        }

        Collider2D hitCollider = Physics2D.OverlapCircle(spawnLocation, 1, enemySpawningLayerMask);
        
        float tryThis = 2f;
        float tryThisToo = 1f;

        while(hitCollider != null)
        {
            if(whichOne)
            {
                spawnLocation = new Vector3(
                playerControl.transform.position.x + tryThisToo, 
                playerControl.transform.position.y - tryThis, 
                playerControl.transform.position.z);
            }
            else
            {
                spawnLocation = new Vector3(
                playerControl.transform.position.x - tryThis, 
                playerControl.transform.position.y + tryThisToo, 
                playerControl.transform.position.z);
            }

            tryThis -= 0.5f;
            tryThisToo *= -1;
            hitCollider = Physics2D.OverlapCircle(spawnLocation, 1, enemySpawningLayerMask);
        }

        GameObject enemy =  whichOne ? 
            Instantiate(HRGuyFred, spawnLocation, Quaternion.identity) : 
            Instantiate(HRGuyJack, spawnLocation, Quaternion.identity);

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
            if(whichOne)
            {
                HRGuyFredObj = enemyObj;
            }
            else
            {
                HRGuyJackObj = enemyObj;
            }
        }

        Weapon enemyWeaponObj = enemy.GetComponent<Weapon>();
        if(enemyWeaponObj != null)
        {
            if(whichOne)
            {
                hRGuyFredWeaponObj = enemyWeaponObj;
            }
            else
            {
                hRGuyJackWeaponObj = enemyWeaponObj;
            }
        }
    }
}
