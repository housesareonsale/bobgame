using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator animator;
    public GameState gameState;
    public bool setAnimation = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        
        if( player != null)
        {
            if(!setAnimation)
            {
                animator.SetBool("Open", true);
                setAnimation = true;
                Invoke("GoToNextLevel", 1f);
            }
        }
    }

    void GoToNextLevel()
    {
        gameState.NextLevel();
    }
}
