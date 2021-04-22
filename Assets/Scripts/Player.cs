using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameState gamestate;
    public GameObject healthBar;
    public ScreenShake screenShake;
    public GameObject  damagePopupComponent;
    public GameObject fireDamageParticle;
    public GameObject deathParticle;
    public float invincibilityMaxTimer = 0.5f;

    bool timeSlowed = false;
    bool invincibility = false;
    float invincibilityTimer = 0f;
    bool died = false;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        invincibilityTimer -= Time.deltaTime;
        if(invincibilityTimer <= 0)
        {
            invincibility = false;
        }
    }

    public void TakeDamage(int damage, bool shake = true, bool burn = false)
    {
        if(burn || !invincibility)
        {
            invincibility = true;
            invincibilityTimer = invincibilityMaxTimer;
            health -= damage;        

            if(shake)
            {
                screenShake.Shake(0.05f);
            }

            GameObject damagePopup = Instantiate(damagePopupComponent, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            DamagePopup damagePopupObj = damagePopup.GetComponent<DamagePopup>();
            damagePopupObj.Setup(damage, true, false);

            if(health < 0){
                if(!died)
                {
                    died = true;
                    Instantiate(deathParticle, transform.position, Quaternion.identity);
                    Die();
                }
            } else {
                HandleHealthBar();
            }
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
        gamestate.LoseGame();
    }

    void SlowTime()
    {
        if(!timeSlowed)
        {
            timeSlowed = true;
            Time.timeScale = 0.25f;
            Invoke("NormalTime", 1f);
        }   
    }
    void NormalTime()
    {
        Time.timeScale = 1f;
    }

    public void HandleHealthBar()
    {
        float amount = (health) / (float)maxHealth;

        healthBar.transform.localScale = new Vector3(
            amount, 
            healthBar.transform.localScale.y, 
            healthBar.transform.localScale.z
        );
    }

    public void InflictBurn(int severity)
    {
        Instantiate(fireDamageParticle, transform.position, Quaternion.identity);
        StartCoroutine(DamageBurn(severity));
    }

    public IEnumerator DamageBurn(int severity)
    {
        var startTime = Time.realtimeSinceStartup;

        while(Time.realtimeSinceStartup < startTime + 0.2f)
        {
            TakeDamage(severity, false, true);
            yield return null;
        }
    }
}
