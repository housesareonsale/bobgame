using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName="GameState", menuName="State/GameState")]
public class GameState : ScriptableObject
{
    public int maxGameFloor;
    public int gameFloor;
    public int maxNumEnemies;
    public GameController gameController;
    public Transform playerLocation;
    public int attackIncreased;
    public int healthIncreased;
    public int currHealth;
    public float firerateIncreased;
    public int attackcost;
    public int fireratecost;
    public int maxhealthcost;
    public int regencost;
    public int enemyHealthIncrease;
    public int enemyDamageIncrease;
    public float enemyFirerateIncrease;
    public List<UpgradeType> playerWeaponUpgrades;
    public List<EnemyType> enemyTypes;
    public int currentCurreny = 0;


    public void Initialize(int startFloor = 10)
    {
        maxGameFloor = gameFloor = startFloor;
        maxNumEnemies = 5;
        attackIncreased = 0;
        healthIncreased = 0;
        enemyHealthIncrease = 0;
        enemyDamageIncrease = 0;
        enemyFirerateIncrease = 0;
        attackcost = 100;
        fireratecost = 100;
        maxhealthcost = 100;
        regencost = 100;
        playerWeaponUpgrades = new List<UpgradeType>();
        currentCurreny = 0;
        enemyTypes = new List<EnemyType>(); 
        enemyTypes.Add(EnemyType.BAD_MAN);
    }

    public void NextLevel(bool exiting)
    {
        if(!exiting)
        {
            if(gameFloor < (int)(maxGameFloor*0.75))
            {
                enemyTypes.Add(EnemyType.SPRAY_MAN);
            }
            else if (gameFloor < (int)(maxGameFloor*0.50))
            {
                enemyTypes.Add(EnemyType.HR_MAN);
            }
            else if (gameFloor < (int)(maxGameFloor*0.25))
            {
                enemyTypes.Add(EnemyType.WIZ_MAN);
            }
            else
            {
                enemyTypes.Add(EnemyType.BAD_MAN);
            }

            SceneManager.LoadScene("Elevator");
        }
        else
        {
            currHealth = gameController.playerControl.player.health;
            int randUpgrade = Random.Range(0,3);

            enemyDamageIncrease += 20;
            enemyHealthIncrease += 100;
            enemyFirerateIncrease -= 0.06f;

            gameFloor -= 1;

            if(gameFloor == (int)(maxGameFloor*0.75))
            {
                SceneManager.LoadScene("MonsterParty"); 
            }
            else if (gameFloor == (int)(maxGameFloor*0.50))
            {
                SceneManager.LoadScene("BenDover");
            }
            else if (gameFloor == (int)(maxGameFloor*0.25))
            {
                SceneManager.LoadScene("TwinBoss");
            }
            else if (gameFloor == 0)
            {
                SceneManager.LoadScene("FinalBoss");
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void EnemyDied(int currenyDrop)
    {
        currentCurreny += currenyDrop;
        gameController.EnemyDied();
    }

    public void StartGame(){
        SceneManager.LoadScene("Start");
    }

    public void LoseGame(){
        SceneManager.LoadScene("Lose");
    }

    public void MainScreen(){
        SceneManager.LoadScene("Main Screen");
    }

    public void UpgradePlayerAttack()
    {
        gameController.UpgradePlayerAttack();
    }

    public void UpgradePlayerFireRate()
    {
        gameController.UpgradePlayerFireRate();
    }

    public void UpgradePlayerHealth(int amoutincrease)
    {
        gameController.UpgradePlayerHealh(amoutincrease, amoutincrease);
    }

    public void RegenPlayerAttack(int amoutregen)
    {
        gameController.UpgradePlayerHealh(amoutregen);
    }

    public void WeaponUpgradeCollected(UpgradeType upgradeType)
    {
        playerWeaponUpgrades.Add(upgradeType);
        gameController.playerControl.UpgradePlayerWeapon(upgradeType);
    }

    public EnemyType GetEnemyToSpawn()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Count)];
    }
}
