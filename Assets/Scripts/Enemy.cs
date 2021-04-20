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
    public EnemyAnimator enemyAnimator;
    public LayerMask playerLayer;
    public int enemyDamage = 5;
    public Vector3 currTargetPosition;
    public GameState gameState;
    public int currenyDrop = 20;
    public GameObject healthBar;
    public ScreenShake screenShake;
    public GameObject enemyDeathParticle;

    // Unit for firerate is frame per shots, so increaing firerate will reduce the number
    // of bullets being spawned per second by the enemy.  
    public float firerate;


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
        enemyAnimator.firerate = firerate;
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
                    Attack();
                }

                break;
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
        healthBar.transform.localScale = new Vector3((health) / 100f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (health <= 0)
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
            Die();
        }
    }

    void Die()
    {
        screenShake.Shake(0.01f);
        gameState.EnemyDied(currenyDrop);
        Instantiate(enemyDeathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject,0.05f);
    }
    # endregion

    public void UpdateCurrencyDrop(float percentIncrease)
    {
        float currencyFloat = currenyDrop * (1 + percentIncrease);
        currenyDrop = (int)currencyFloat;
    }
}
