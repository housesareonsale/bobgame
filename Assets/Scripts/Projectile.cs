using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet has collided");
        Destroy(gameObject);
    }
}
