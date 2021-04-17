using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMan : Enemy
{
    public Weapon weapon;    
    public Transform firepoint;

    void Start()
    {
        weapon.animator = animator;
    }
    public override void Attack()
    {
        EnemyAttack();
    }
    void EnemyAttack()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("ShawdowBoiShoot")) {

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
        firepoint.right = currTargetPosition - transform.position;
    }
}
