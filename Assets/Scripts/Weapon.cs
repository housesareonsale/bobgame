using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject projectile;
    public GameObject effectProjectile;
    public GameObject shooter;
    public int damage = 1000;
    public float projectileForce = 20f;
    public Animator animator;
    public EnemyAnimator enemyAnimator;
    public float projectileDuration = 2;
    public bool friendly;

    int numProjectiles;
    float sizeIncrease;
    int fireEffect;

    void Start()
    {
        sizeIncrease = 1f;
        fireEffect = 0;
        numProjectiles = 1;
    }

    public void Shoot(Vector3? optionalDirection = null, bool notFromSlug = true)
    {
        Vector3 position = firepoint.position;
        Vector3 direction = firepoint.right;
        if(optionalDirection != null)
        {
            direction = optionalDirection.Value;
        }

        if(animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("BoobsAttack"))
            animator.SetTrigger("Shoot");

        if(enemyAnimator != null)
        {
            enemyAnimator.TriggerShoot();
        }

        if(numProjectiles <= 1 || !notFromSlug)
        {
            GameObject bullet;

            if(fireEffect == 0)
            {
                bullet = Instantiate(projectile, position, firepoint.rotation);
            }
            else
            {
                bullet = Instantiate(effectProjectile, position, firepoint.rotation);
                EffectProjectile effectObj = bullet.GetComponent<EffectProjectile>();
                effectObj.level = fireEffect;
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            var bulletObj = bullet.GetComponent<Projectile>();
            bulletObj.damage = damage;
            bulletObj.friendly = friendly;
            bulletObj.projectileDuration = projectileDuration;
            bullet.transform.localScale *= sizeIncrease;

            // Ignores collisions with caster and collision
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>());
            rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
        }
        else if(notFromSlug)
        {
            SlugShot(numProjectiles);
        }
    }

    public void SlugShot(int numBullets)
    {
        for(int i = 0; i < numBullets; i++)
        {
            Vector3 offset = new Vector3((Random.Range(1f, 3f) / 10), (Random.Range(0.5f, 0.8f) / 10), 0) * sizeIncrease;
            Vector3 direction = firepoint.right + offset;
            Shoot(direction, false);
        }
    }

    public void UpgradeWeapon(UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            default:
            case UpgradeType.NUM_PROJECTILES:
                numProjectiles++;
                break;
            case UpgradeType.BIGGER_PROJECTILES:
                sizeIncrease += 0.5f;
                break;
            case UpgradeType.FIRE:
                fireEffect += 1;
                break;
        }
    }

    public void UpgradeBulk(List<UpgradeType> upgradeTypes)
    {
        foreach(UpgradeType upgradeType in upgradeTypes)
        {
            UpgradeWeapon(upgradeType);
        }
    }
}
