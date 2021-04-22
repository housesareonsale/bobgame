using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public GameController gameController;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObj = collision.gameObject;

        // If the projectile collided with a player damage it
        Player player = gameObj.GetComponent<Player>();
        
        if(player != null)
        {
            gameController.CotinueEvent();
            Destroy(gameObject);
        }
    }
}
