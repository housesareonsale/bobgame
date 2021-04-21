using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayGuy : Enemy
{
    public Weapon weapon;
    public Transform firepoint;
    public float accuracy;
    public float projectileDuration;
    public int numBullets = 4;

    Transform enemyPosition;

    void Start()
    {
        weapon.enemyAnimator = enemyAnimator;
        weapon.friendly = false;
        weapon.projectileDuration = projectileDuration;
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
                weapon.SlugShot(numBullets);
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
}
