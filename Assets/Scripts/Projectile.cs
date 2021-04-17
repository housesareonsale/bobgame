using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;
    public int damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObj = collision.gameObject;
        Debug.Log(gameObj.name);
        Debug.Log(friendly);
        if(friendly)
        {
            // If the projectile collided with an enemy damage it
            Enemy enemy = gameObj.GetComponent<Enemy>();
            if(enemy != null) {
                Debug.Log("enemy not null");
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            // If the projectile collided with a player damage it
            Player player = gameObj.GetComponent<Player>();
            
            if(player != null) {
                Debug.Log("player hit");
                player.TakeDamage(damage);
            }
        }

        Destroy(gameObject);

    }

   
}
