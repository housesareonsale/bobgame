using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 20f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Weapon playerWeapon;
    public Camera cam;

    Vector3 movement;
    Vector2 mousePointer;
    bool facingRight = true;
    bool isShooting = false;
    
    void Start()
    {
        playerWeapon.animator = animator;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        isShooting = !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerShoot");
        mousePointer = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if(Input.GetButton("Fire1"))
        {
            playerWeapon.Shoot();
        }

        if(movement.x != 0 || movement.y != 0) 
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

        float actualMoveSpeed = !isShooting ? moveSpeed/2 : moveSpeed;
        Vector2 moved = movement * actualMoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moved);

        Vector2 pointDir = mousePointer - rb.position;
        float angle = Mathf.Atan2(pointDir.y, pointDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
