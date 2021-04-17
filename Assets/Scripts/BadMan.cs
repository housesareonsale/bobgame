using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMan : Enemy
{
    public Weapon weapon;    
    public Transform firepoint;
    public float accuracy;

    void Start()
    {
        weapon.animator = animator;
        weapon.friendly = false;
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
        float aimx =  Random.Range(-accuracy,accuracy);
        float aimy =  Random.Range(-accuracy,accuracy);
        firepoint.right = currTargetPosition - transform.position + new Vector3(aimx,aimy,0);
    }
}
