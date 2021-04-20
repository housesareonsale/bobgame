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


    public int currentCurreny = 0;

    public void Initialize(int startFloor = 20)
    {
        gameFloor = startFloor;
        maxGameFloor = gameFloor = startFloor;
        maxNumEnemies = 0;
        attackcost = 100;
        fireratecost = 100;
    }

    public void NextLevel()
    {
        int randUpgrade = Random.Range(0,3);

        switch(randUpgrade) 
        {
            default:
            case 0:
                enemyDamageIncrease += 5;
                break;
            case 1:
                enemyHealthIncrease += 8;
                break;
            case 2:
                enemyFirerateIncrease -= 0.01f;
                break;
        }

        gameFloor -= 1;
        SceneManager.LoadScene("Game");
    }

    public void EnemyDied(int currenyDrop)
    {
        currentCurreny += currenyDrop;
        gameController.EnemyDied();
    }

    public void StartGame(){
        SceneManager.LoadScene("Game");
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
}
