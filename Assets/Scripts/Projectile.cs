using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;
    public int damage;
    public float projectileDuration;

    private void Start()
    {
        Invoke("DestroyProjectile", projectileDuration);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObj = collision.gameObject;

        if(friendly)
        {
            // If the projectile collided with an enemy damage it
            Enemy enemy = gameObj.GetComponent<Enemy>();
            if(enemy != null) {
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            // If the projectile collided with a player damage it
            Player player = gameObj.GetComponent<Player>();
            
            if(player != null) {
                player.TakeDamage(damage);
            }
        }

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        // play some sound effect / add particle effects
        Destroy(gameObject);
    }
}
