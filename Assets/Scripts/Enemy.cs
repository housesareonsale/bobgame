using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public GameObject damagePopupComponent;
    public GameObject fireDamageParticle;
    public int maxHealth;
    public float timeTillDespawn = 5f; 
    public bool boss = false;
    public bool cutScene = false;
    public bool dead = false;
    public AudioSource audioSource;
    public AudioClip enemyDeathSound;

    [Header("Events")]
    [Space]
    public UnityEvent deathEvent;

    // Unit for firerate is frame per shots, so increaing firerate will reduce the number
    // of bullets being spawned per second by the enemy.  
    public float firerate;

    float timer;
    Vector3 startPosition;
    Vector3 roamPosition;
    EnemyState state = EnemyState.ROAMING;
    enum EnemyState {
        ROAMING, CHASING, ATTACKING
    }

    void Awake()
    {
        if (deathEvent == null)
            deathEvent = new UnityEvent();
    }

    void Start()
    {
        startPosition = transform.position;
        roamPosition = Util.GetRandomPosition(startPosition);
        currTargetPosition = roamPosition;
        enemyMovement.speed = moveSpeed;
        enemyAnimator.firerate = firerate;
        enemyAnimator.isBoss = boss;
        maxHealth = health;
        timer = timeTillDespawn;
    }

    void Update()
    {
        if(!cutScene)
        {
            if(timeTillDespawn <= 0 && !boss)
            {
                Die(true);
            }

            switch(state)
            {
                default:
                case EnemyState.ROAMING:
                    enemyMovement.SetTarget(currTargetPosition);

                    float reachedPosition = 10f;
                    timeTillDespawn -= Time.deltaTime;

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

                    timer = timeTillDespawn;

                    if(distance <= attackRange)
                    {
                        enemyMovement.StopMove();
                        Attack();
                    }

                    break;
            }
        }
        else
        {
            enemyMovement.StopMove();
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
    public void TakeDamage(int damage, bool burn = false)
    {
        if(!cutScene)
        {
            health -= damage;

            var renderer = gameObject.GetComponent<Renderer>();
            DamageExtra();

            GameObject damagePopup = Instantiate(damagePopupComponent, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            DamagePopup damagePopupObj = damagePopup.GetComponent<DamagePopup>();
            damagePopupObj.Setup(damage, false, burn);

            float amount = (health) / (float)maxHealth;
            healthBar.transform.localScale = new Vector3(
                amount, 
                healthBar.transform.localScale.y, 
                healthBar.transform.localScale.z
            );

            if (health <= 0)
            {
                Die(false);
            }
        }
    }

    public void TazerHit(int damage)
    {
        health -= damage;
        // Play exploding animation

        // Delete the object if the health is zero
        if(health <= 0)
        {
            Die(false);
        }
    }

    void Die(bool despawn = false)
    {
        if(!despawn)
        {
            screenShake.Shake(0.01f);
            Instantiate(enemyDeathParticle, transform.position, Quaternion.identity);
        }

        if(!dead)
        {
            int currenyToDrop = despawn ? 0 : currenyDrop;
            dead = true;
            if(deathEvent != null)
            {
                deathEvent.Invoke();
            }

            gameState.EnemyDied(currenyToDrop, enemyDeathSound);
            Destroy(gameObject);
        }
    }

    public virtual void DamageExtra()
    {

    } 
    # endregion

    public void UpdateCurrencyDrop(float percentIncrease)
    {
        float currencyFloat = currenyDrop * (1 + percentIncrease);
        currenyDrop = (int)currencyFloat;
    }

    public void InflictBurn(int severity)
    {
        Instantiate(fireDamageParticle, transform.position, Quaternion.identity);
        StartCoroutine(DamageBurn(severity));
    }

    public IEnumerator DamageBurn(int severity)
    {
        var startTime = Time.realtimeSinceStartup;

        while(Time.realtimeSinceStartup < startTime + 1f)
        {
            TakeDamage(severity, true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
