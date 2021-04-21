using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenDover : Enemy
{
    public Weapon weapon;
    public Transform firepoint;
    public float accuracy;
    public float projectileDuration;

    Transform enemyPosition;
    bool messaged = false;
    void Start()
    {
        weapon.enemyAnimator = enemyAnimator;
        weapon.friendly = false;
        weapon.projectileDuration = projectileDuration;
        weapon.fireEffect = 3;
        enemyPosition = GetComponent<Transform>();
    }
    public override void Attack()
    {
        EnemyAttack();
    }

    void EnemyAttack()
    {
        if (!enemyAnimator.IsAttacking())
        {

            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
            foreach (Collider2D player in hitPlayer)
            {
                Target();
                weapon.SlugShot();
                weapon.MultiDirShot();
            }
        }
    }

    void Target()
    {
        float aimx = Random.Range(-accuracy, accuracy);
        float aimy = Random.Range(-accuracy, accuracy);
        firepoint.right = currTargetPosition - transform.position + new Vector3(aimx, aimy, 0);

        enemyMovement.ScaleEnemy(firepoint.right.x >= 0.01f, firepoint.right.x <= -0.01f);
    }

    public override void DamageExtra()
    {
        if(health <= (maxHealth/2) && !messaged)
        {
            messaged = true;
            if(deathEvent != null)
            {
                deathEvent.Invoke();
            }
        }
    }
}
