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
    public int currentCurreny = 0;


    public void Initialize(int startFloor = 20)
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
    }

    public void NextLevel()
    {
        currHealth = gameController.playerControl.player.health;
        int randUpgrade = Random.Range(0,3);

        enemyDamageIncrease += 15;
        enemyHealthIncrease += 28;
        enemyFirerateIncrease -= 0.04f;

        gameFloor -= 1;

        if(gameFloor == (int)(maxGameFloor*0.75))
        {
            // MEET 5 SNIPERS
        }
        else if (gameFloor == (int)(maxGameFloor*0.50))
        {
            // MEET BEN DOVER
        }
        else if (gameFloor == (int)(maxGameFloor*0.25))
        {
            // TWINS
        }
        else if (gameFloor == 0)
        {
            // FINAL BOSS
        }
        else
        {
            SceneManager.LoadScene("Game");
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
}
