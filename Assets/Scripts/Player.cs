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
    public ScreenShake screenShake;

    void Start()
    {
        health = maxHealth;
        healthBarXPos = healthBar.transform.position.x;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;        

        HandleHealthBar();
        if(health < 0){
            Die();
        }
    }

    public void UpdatePlayerHealth(int currChange, int maxChange)
    {
        maxHealth += maxChange;
        health = Mathf.Min(maxHealth, health + currChange);

        HandleHealthBar();
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

    void HandleHealthBar()
    {
        float amount = (health) / (float)maxHealth;

        screenShake.Shake(0.05f);
        healthBar.transform.localScale = new Vector3(
            amount, 
            healthBar.transform.localScale.y, 
            healthBar.transform.localScale.z
        );
    }

}
