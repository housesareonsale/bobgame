using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator face;
    public Animator body;
    public Animator leg;
    public Animator boss;

    public SpriteRenderer faceRenderer;
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer legRenderer;
    
    public bool isBoss;
    public float firerate;
    bool isAttacking = false;

    public void SetMoveSpeed(float moveSpeed)
    {
        if(!isBoss)
        {   
            if(!IsAttacking())
            {
                face.SetFloat("MoveSpeed", moveSpeed);
                body.SetFloat("MoveSpeed", moveSpeed);
                leg.SetFloat("MoveSpeed", moveSpeed);
            }
        }
        else
        {
            // This will handle boss animation
            boss.SetFloat("MoveSpeed", moveSpeed);
        }
    }

    public void TriggerShoot()
    {
        if(!IsAttacking())
        {
            if(!isBoss)
            {
                face.SetTrigger("Shoot");
                body.SetTrigger("Shoot");
                leg.SetTrigger("Shoot");

                Invoke("DoneAttacking", firerate);
                isAttacking = true;
            }
            else
            {
                boss.SetTrigger("Shoot");
                Invoke("DoneAttacking", firerate);
                isAttacking = true;
            }
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
