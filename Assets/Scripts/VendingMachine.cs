using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public GameState gameState;
    public GameObject textPopupComponent;
    public AudioSource audioSource;
    public AudioClip audioClip;

    bool upgraded = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObj = collision.gameObject;
        Player player = gameObj.GetComponent<Player>();
        
        if(player != null)
        {
            if(gameState.playerWeaponUpgrades == null || gameState.playerWeaponUpgrades.Count == 0)
            {
                GameObject textPopup = Instantiate(textPopupComponent, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                DamagePopup textPopupObj = textPopup.GetComponent<DamagePopup>();
                textPopupObj.SetupVendingMachine();
                Destroy(textPopup, 6f);
            }
        }

    }

    public void CollectWeaponUpgrade()
    {
        if(!upgraded)
        {
            int upgradeChoice = Random.Range(0, 3);
            UpgradeType upgradeType;

            if(upgradeChoice == 0) 
            {
                upgradeType = UpgradeType.NUM_PROJECTILES;
            }
            else if(upgradeChoice == 1) 
            {
                upgradeType = UpgradeType.BIGGER_PROJECTILES;
            }
            else 
            {
                upgradeType = UpgradeType.FIRE;
            }


            gameState.WeaponUpgradeCollected(upgradeType);

            GameObject textPopup = Instantiate(textPopupComponent, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            DamagePopup textPopupObj = textPopup.GetComponent<DamagePopup>();
            textPopupObj.SetupVendingMachineUpgrade(upgradeType);
            audioSource.PlayOneShot(audioClip, 0.50f);

            upgraded = true;
        }
    } 
}
