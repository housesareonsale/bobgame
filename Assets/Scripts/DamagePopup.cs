using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private static int sortingOrder = 0;
    public TextMeshPro textMesh;
    public Vector3 moveVector;
    public float maxDissapearTime = 1f;

    bool damageText = true;
    float dissapearTimer = 0.1f; 
    Color textColor;

    void Await()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool playerDamage, bool burn)
    {
        damageText = true;
        textMesh.text = damageAmount.ToString();
        moveVector = new Vector3(0.7f, 1) * 0.2f; 

        if(!playerDamage)
        {
            textColor = new Color32(230, 230 , 0, 255);

        }
        else if(burn)
        {
            textColor = new Color32(255, 125 , 0, 255);
        }
        else
        {
            textColor = new Color32(255, 36 , 0, 255);
            moveVector += new Vector3(-1.4f, 0, 0);
        }

        textMesh.faceColor = textColor;
        dissapearTimer = maxDissapearTime;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

    }

    void Update()
    {
        if(damageText)
        {
            transform.position += moveVector * Time.deltaTime;

            float changedScaleAmout = 0.02f; 
            if(dissapearTimer > (maxDissapearTime * 0.5f))
            {
                transform.localScale += Vector3.one * changedScaleAmout * Time.deltaTime;
            }
            else
            {
                transform.localScale -= Vector3.one * changedScaleAmout * Time.deltaTime;
            }

            dissapearTimer -= Time.deltaTime;
            if(dissapearTimer < 0)
            {
                // Start dissapearing
                float dissapearSpeed = 3f;
                textColor.a -= dissapearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if(textColor.a <= 0)
                {
                    Destroy(gameObject); 
                }
            }
        }
    }

    public void SetupVendingMachine()
    {
        damageText = false;
        textMesh.text = "Not saying you should press 'E'. \nBut if I was next to a Vending Machine in the middle of an apocalypse I would try to press 'E' if I could possibly get a weapon upgrade.";

        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);

        textColor = new Color32(221, 160, 221,255);
        textMesh.faceColor = textColor;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    public void SetUpLockedElevator()
    {
        damageText = false;
        textMesh.text = "I think I gotta finish the tasks on this level before heading down, otherwise I'm scared the elevator might crash.";

        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);

        textColor = new Color32(221, 160, 221,255);
        textMesh.faceColor = textColor;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    public void SetUpTwinOneBeat()
    {
        damageText = false;
        textMesh.text = "Looks like offing one of them supercharged the other one.";

        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);

        textColor = new Color32(221, 160, 221,255);
        textMesh.faceColor = textColor;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    public void SetUpBenDover()
    {
        damageText = false;
        textMesh.text = "Please come back to your senses Ben. I don't wanna off you..";

        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);

        textColor = new Color32(221, 160, 221,255);
        textMesh.faceColor = textColor;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    public void SetUpUnlockedElevator()
    {
        damageText = false;
        textMesh.text = "Looks like the elevator door just opened, I can finally escape this hord.";

        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);

        textColor = new Color32(221, 160, 221,255);
        textMesh.faceColor = textColor;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    public void SetupVendingMachineUpgrade(UpgradeType upgradeType)
    {
        damageText = false;

        switch(upgradeType)
        {
            default:
            case UpgradeType.NUM_PROJECTILES:
                textMesh.text = "Ah cool! more stapler pins, I can now shoot more at a time.";
                break;
            case UpgradeType.BIGGER_PROJECTILES:
                textMesh.text = "Nice this new stapler pin box seems to have bigger stapler pins.";
                break;
            case UpgradeType.FIRE:
                textMesh.text = "Goddam match sticks, I can use this to turn my stapler pins into firey pins.";
                break;
        }

        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);

        textColor = new Color32(221, 160, 221,255);
        textMesh.faceColor = textColor;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;    
    }
}
