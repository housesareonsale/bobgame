﻿using System.Collections;
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
    public bool friendly;


    public void Shoot()
    {
        if(animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("BoobsAttack"))
            animator.SetTrigger("Shoot");

        if(enemyAnimator != null)
        {
            enemyAnimator.TriggerShoot();
        }

        GameObject bullet = Instantiate(projectile, firepoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Projectile bulletObj = bullet.GetComponent<Projectile>();
        bulletObj.damage = damage;
        bulletObj.friendly = friendly;

        // Ignores collisions with caster and collision
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>());
        rb.AddForce(firepoint.right * projectileForce, ForceMode2D.Impulse);
    }

}
