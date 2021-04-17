using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int speicalGained = 25;
    public float reachedPosition = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 200f;
    public float agroRange = 50f;
    public Transform targetPosition;
    public EnemyMovement enemyMovement;
    public Animator animator;
    public LayerMask playerLayer;
    public int enemyDamage = 5;
    public Vector3 currTargetPosition;
    // public GameState gameState;

    Vector3 startPosition;
    Vector3 roamPosition;
    EnemyState state = EnemyState.ROAMING;
    enum EnemyState {
        ROAMING, CHASING, ATTACKING
    }

    void Start()
    {
        startPosition = transform.position;
        roamPosition = Util.GetRandomPosition(startPosition);
        currTargetPosition = roamPosition;
        enemyMovement.speed = moveSpeed;
    }

    void Update()
    {
        switch(state)
        {
            default:
            case EnemyState.ROAMING:
                enemyMovement.SetTarget(currTargetPosition);

                float reachedPosition = 10f;

                if(Vector3.Distance(transform.position, currTargetPosition) < reachedPosition)
                {
                    if(currTargetPosition == startPosition)
                    {
                        currTargetPosition = roamPosition;
                    }
                    else 
                    {
                        currTargetPosition = startPosition;
                    }
                }

                FindTarget();
                break;

            case EnemyState.CHASING:
                enemyMovement.SetTarget(targetPosition.position);

                var distance = Vector3.Distance(transform.position, targetPosition.position);
                FindTarget(2);

                if(distance <= attackRange)
                {
                    enemyMovement.StopMove();
                    state = EnemyState.ATTACKING;
                    Attack();
                }

                break;
            
            case EnemyState.ATTACKING:
                enemyMovement.StopMove();
                state = EnemyState.ATTACKING;
                Attack();

                break;
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("RockyMonsterAttack"))
        {
            enemyMovement.ContinueMove();
            FindTarget(2);
        }
    }

    public virtual void Attack()
    {

    }

    # region Enemy Movement
    void FindTarget(float addtionalDist = 0)
    {
        if(Vector3.Distance(transform.position, targetPosition.position) <= agroRange + addtionalDist)
        {
            state = EnemyState.CHASING;    
            currTargetPosition = targetPosition.position;
        }
        else
        {
            state = EnemyState.ROAMING;
        }
    }
    #endregion

    # region Enemy Health
    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    public void TazerHit(int damage)
    {
        health -= damage;
        // Play exploding animation

        // Delete the object if the health is zero
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Die()
    {
        // gameState.EnemyDied(speicalGained);
        // audioSource.PlayOneShot(enemyDeath, 0.10f);
        Destroy(gameObject, 0.5f);
    }
    # endregion
}
