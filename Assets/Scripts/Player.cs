using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health, maxHealth;
    public GameState gamestate;
    public GameObject healthBar;

    //public Transform position;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health = 100;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int x){
        health -= x;
        healthBar.transform.localScale = new Vector3((health) / 100f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if(health < 0){
            Die();
        }
    }

    public void ResetHealth(){
        health=maxHealth;
    }

    void Die(){
        gamestate.LoseGame();
    }

}
