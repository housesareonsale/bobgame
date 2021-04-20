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

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;        

        GameObject damagePopup = Instantiate(damagePopupComponent, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        DamagePopup damagePopupObj = damagePopup.GetComponent<DamagePopup>();
        damagePopupObj.Setup(damage, true, false);

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
            TakeDamage(severity);
            yield return null;
        }
    }
}
