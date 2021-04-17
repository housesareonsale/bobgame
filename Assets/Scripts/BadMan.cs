using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMan : Enemy
{
    public Weapon weapon;    
    public Transform firepoint;
    public float accuracy;

    Transform enemyPosition;

    void Start()
    {
        weapon.enemyAnimator = enemyAnimator;
        weapon.friendly = false;
        enemyPosition = GetComponent<Transform>();
    }
    public override void Attack()
    {
        EnemyAttack();
    }
    void EnemyAttack()
    {
        if(!enemyAnimator.IsAttacking()) {

            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
            foreach(Collider2D player in hitPlayer)
            {
                Target();
                weapon.Shoot();
            }
        }
    }

    void Target()
    {
        float aimx =  Random.Range(-accuracy,accuracy);
        float aimy =  Random.Range(-accuracy,accuracy);
        firepoint.right = currTargetPosition - transform.position + new Vector3(aimx,aimy,0);

        if (firepoint.right.x >= 0.01f)
        {
            enemyPosition.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (firepoint.right.x <= -0.01f)
        {
            enemyPosition.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
