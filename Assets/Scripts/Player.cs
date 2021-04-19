using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameState gamestate;
    public GameObject healthBar;
    public float healthBarXPos;

    void Start()
    {
        health = maxHealth;
        healthBarXPos = healthBar.transform.position.x;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        float amount = (health) / (float)maxHealth;

        healthBar.transform.localScale = new Vector3(
            amount, 
            healthBar.transform.localScale.y, 
            healthBar.transform.localScale.z
        );

        if(health < 0){
            Die();
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    void Die()
    {
        // gamestate.LoseGame();
    }

}
