using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health, maxHealth;

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
        Debug.Log("" + x);
        if(health < 0){
            Die();
        }
    }

    public void ResetHealth(){
        health=maxHealth;
    }

    void Die(){
        Debug.Log("player died");
        Destroy(gameObject, 0.5f);
    }
}
