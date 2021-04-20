using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string[] weaponmods;
    public Transform firepoint;
    public GameObject projectile;
    public GameObject shooter;
    public int damage = 1000;
    public float projectileForce = 20f;
    public Animator animator;
    public EnemyAnimator enemyAnimator;
    public float projectileDuration = 2;
    public bool friendly;


    public void Shoot(Vector3? optionalPosition = null)
    {
        Vector3 position = firepoint.position;
        if(optionalPosition != null)
        {
            position = optionalPosition.Value;
        }

        if(animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("BoobsAttack"))
            animator.SetTrigger("Shoot");

        if(enemyAnimator != null)
        {
            enemyAnimator.TriggerShoot();
        }

        GameObject bullet = Instantiate(projectile, position, firepoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Projectile bulletObj = bullet.GetComponent<Projectile>();
        bulletObj.damage = damage;
        bulletObj.friendly = friendly;
        bulletObj.projectileDuration = projectileDuration;

        // Ignores collisions with caster and collision
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>());
        rb.AddForce(firepoint.right * projectileForce, ForceMode2D.Impulse);
    }

    public void SlugShot(int numBullets)
    {
        for(int i = 0; i < numBullets; i++)
        {
            Vector3 offset = new Vector3(Random.Range(6f, 8.5f) / 10, Random.Range(6f, 8.5f) / 10, 0);
            Vector3 position = firepoint.right + offset;
            Shoot();
        }
    }
}
