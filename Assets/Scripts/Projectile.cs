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
        GameObject other = collision.otherCollider.gameObject;
        if(gameObj.GetComponent<Projectile>() != null){
            Debug.Log("collision man come on dude");
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }else{
            Debug.Log("Bullet has collided");

            
            if(friendly)
            {
                Debug.Log("Friendly");
                Debug.Log(gameObj);
                // If the projectile collided with an enemy damage it
                Enemy enemy = gameObj.GetComponent<Enemy>();
                if(enemy != null) {
                    Debug.Log("enemy not null");
                    enemy.TakeDamage(damage);
                }
            }
            else
            {
                Debug.Log(" Not Friendly");
                // If the projectile collided with a player damage it
                Player player = gameObj.GetComponent<Player>();
                if(player != null) {
                    player.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
    }

   
}
