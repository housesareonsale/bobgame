using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int health;
    public GameObject furnitureCollision;
    public GameObject furnitureDestroy;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(furnitureCollision, transform.position, Quaternion.identity);
        
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(furnitureDestroy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
