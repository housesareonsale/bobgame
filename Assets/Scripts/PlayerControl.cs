using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 20f;
    public Rigidbody2D rb;
    public Rigidbody2D weaponRb;
    public SpriteRenderer spriteRenderer;
    public Weapon playerWeapon;
    public Camera cam;
    public Transform firepoint;
    public Transform weaponPosition;
    public bool levelGenerationDone = false;
    public float fireRate = 0.5f;
    public float maxTime = 8f;
    public float timer = 8f; 
    public Player player;
    public LayerMask vendingMachineLayer;

    Vector3 movement;
    Vector2 mousePointer;
    bool facingRight = true;
    bool isShooting = false;
    
    void Start()
    {
        playerWeapon.friendly = true;
    }

    void Update()
    {
        if(levelGenerationDone)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //hugh.normalize(bitch)
            if(!(movement.x == 0 && movement.y == 0))
            {
                float magn = (float) Math.Sqrt(Math.Pow(movement.x,2) + Math.Pow(movement.y,2));
                movement.x /= magn;
                movement.y /= magn;
            }

            isShooting = !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerShoot");
            mousePointer = cam.ScreenToWorldPoint(Input.mousePosition);

            if(Input.GetButton("Interact"))
            {
                TryWeaponUpgrade();
            }
        }
    }

    void FixedUpdate()
    {
        if(levelGenerationDone)
        {
            if(timer > 0)
            {
                timer -= fireRate;
            }

            if(Input.GetButton("Fire1"))
            {
                Attack();
            }

            if(Mathf.Abs(movement.x) >= 0.01 || Mathf.Abs(movement.y) >= 0.01) 
            {
                animator.SetFloat("MoveSpeed", moveSpeed);
            }
            else
            {
                animator.SetFloat("MoveSpeed", 0f);
            }

            if(movement.x > 0.01 && !facingRight)
            {
                spriteRenderer.flipX = false;
                facingRight = true;
            }
            else if (movement.x < -0.01 && facingRight)
            {
                spriteRenderer.flipX = true;
                facingRight = false;
            }

            float actualMoveSpeed = moveSpeed;
            Vector2 moved = movement * actualMoveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + moved);
            weaponRb.MovePosition(new Vector2(weaponPosition.position.x, weaponPosition.position.y) + moved);

            Vector2 pointDir = mousePointer - rb.position;
            float angle = Mathf.Atan2(pointDir.y, pointDir.x) * Mathf.Rad2Deg;
            weaponRb.rotation = angle;
        }
    }

    void Attack()
    {
        if(timer <= 0)
        {
            animator.SetTrigger("Shoot");
            playerWeapon.Shoot();
            timer = maxTime;
        }
    }

    public void UpdatePlayerHealth(int currIncrease, int maxIncrease)
    {
        player.UpdatePlayerHealth(currIncrease, maxIncrease);
    }

    public void UpgradePlayerWeapon(UpgradeType upgradeType)
    {
        playerWeapon.UpgradeWeapon(upgradeType);
    }

    public void TryWeaponUpgrade()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 1.5f, vendingMachineLayer);
        if(collision != null)
        {
            GameObject collidedObj = collision.gameObject;
            VendingMachine vendingMachine = collidedObj.GetComponent<VendingMachine>();
            vendingMachine.CollectWeaponUpgrade();
        }
    }
}
