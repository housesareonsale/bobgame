using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName="GameState", menuName="State/GameState")]
public class GameState : ScriptableObject
{
    public int gameFloor;
    public int maxNumEnemies;
    public GameController gameController;
    public Transform playerLocation;
    public int attackIncreased;
    public float firerateIncreased;


    public int currentCurreny = 0;

    public void Initialize(int startFloor = 20)
    {
        gameFloor = startFloor;
        maxNumEnemies = 0;
    }

    public void NextLevel()
    {
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
        attackIncreased += 7;
        gameController.UpgradePlayerAttack();
    }

    public void UpgradePlayerFireRate()
    {
        firerateIncreased += 0.08f;
        gameController.UpgradePlayerFireRate();
    }
}
