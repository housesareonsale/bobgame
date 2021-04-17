using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator face;
    public Animator body;
    public Animator leg;

    public float firerate;
    bool isAttacking = false;

    public void SetMoveSpeed(float moveSpeed)
    {
        if(!IsAttacking())
        {
            face.SetFloat("MoveSpeed", moveSpeed);
            body.SetFloat("MoveSpeed", moveSpeed);
            leg.SetFloat("MoveSpeed", moveSpeed);
        }
    }

    public void TriggerShoot()
    {
        if(!IsAttacking())
        {
            face.SetTrigger("Shoot");
            body.SetTrigger("Shoot");
            leg.SetTrigger("Shoot");

            Invoke("DoneAttacking", firerate);
            isAttacking = true;
        }
    }

    public void DoneAttacking()
    {
        isAttacking = false;
    }

    public bool IsAttacking()
    {
        // Check if the enemy is attacking here
        return isAttacking;
    }
}
