using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool locked = false;
    public Animator animator;
    public GameState gameState;
    public GameObject textPopupComponent;
    public bool setAnimation = false;
    public bool exiting = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        
        if( player != null && !locked )
        {
            if(!setAnimation)
            {
                animator.SetBool("Open", true);
                setAnimation = true;
                Invoke("GoToNextLevel", 1f);
            }
        }

        if(locked)
        {
            GameObject textPopup = Instantiate(textPopupComponent, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            DamagePopup textPopupObj = textPopup.GetComponent<DamagePopup>();
            textPopupObj.SetUpUnlockedElevator();
            Destroy(textPopup, 8f);
        }
    }

    void GoToNextLevel()
    {
        gameState.NextLevel(exiting);
    }
}
