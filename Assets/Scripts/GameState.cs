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


    int currentCurreny = 0;

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
}
