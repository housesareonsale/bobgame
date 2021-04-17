using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public string[] weaponmods;
    public Transform firepoint;
    public GameObject projectile;
    public GameObject shooter;
    public int damage = 100;
    public float projectileForce = 20f;
    public Animator animator;


    public void Shoot()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerShoot"))
        {
            animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(projectile, firepoint.position, firepoint.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Projectile bulletObj = bullet.GetComponent<Projectile>();
            bulletObj.damage = damage;

            // Ignores collisions with caster and collision
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>());
            rb.AddForce(firepoint.right * projectileForce, ForceMode2D.Impulse);
        }
    }

}
