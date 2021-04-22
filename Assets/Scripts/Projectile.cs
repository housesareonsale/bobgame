using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;
    public int damage;
    public float projectileDuration;
    public GameObject collisionParticle;
    public AudioSource audioSource;
    public AudioClip startSound;

    private void Start()
    {
        audioSource.PlayOneShot(startSound, 0.10f);
        Invoke("DestroyProjectile", projectileDuration);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObj = collision.gameObject;

        Obstacle obstacle = gameObj.GetComponent<Obstacle>();
        if(obstacle != null)
        {
            DoExtra(gameObj);
            obstacle.TakeDamage(damage);
        }
        else
        {
            if(friendly)
            {
                // If the projectile collided with an enemy damage it
                Enemy enemy = gameObj.GetComponent<Enemy>();
                if(enemy != null)
                {
                    DoExtra(gameObj);
                    enemy.TakeDamage(damage);
                }
            }
            else
            {
                // If the projectile collided with a player damage it
                Player player = gameObj.GetComponent<Player>();
                
                if(player != null)
                {
                    DoExtra(gameObj);
                    player.TakeDamage(damage);
                }
            }
        }

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public virtual void DoExtra(GameObject target)
    {

    }
}
