using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteGenerator : MonoBehaviour
{
    public EnemyParts enemyParts;
    public EnemyPart enemyPart;
    public Animator animator;

    void Start()
    {
        if(enemyPart == EnemyPart.FACE)
        {
            animator.runtimeAnimatorController = enemyParts.face[Random.Range(0, enemyParts.face.Length)].runtimeAnimatorController;
        }

        if(enemyPart == EnemyPart.BODY)
        {
            animator.runtimeAnimatorController = enemyParts.body[Random.Range(0, enemyParts.body.Length)].runtimeAnimatorController;
        }

        if(enemyPart == EnemyPart.LEGS)
        {
            animator.runtimeAnimatorController = enemyParts.leg[Random.Range(0, enemyParts.leg.Length)].runtimeAnimatorController;
        }
    }
}
