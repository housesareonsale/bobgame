using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public GameState gamestate;
    public GameObject panel;
    public TextMeshProUGUI points;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI firerate;
    public TextMeshProUGUI maxhealth;
    public TextMeshProUGUI regenhealth;
    public AudioSource audioSource;
    public AudioClip gotUpgrade;

    void Start()
    {
        attack.text = gamestate.attackcost.ToString();
        firerate.text = gamestate.fireratecost.ToString();
        maxhealth.text = gamestate.maxhealthcost.ToString();
        regenhealth.text = gamestate.regencost.ToString();
    }

    void Update()
    {
        points.text = gamestate.currentCurreny.ToString();
    }

    public void OnAttackUpgrade()
    {
        if(gamestate.attackcost <= gamestate.currentCurreny)
        {
            int attackAmoutIncreased = 7;
            float attackcostIncrease = 2f;

            gamestate.currentCurreny -= gamestate.attackcost;
            gamestate.UpgradePlayerAttack();
            gamestate.attackIncreased += attackAmoutIncreased;
            gamestate.attackcost = (int)(gamestate.attackcost * attackcostIncrease);
            attack.text = gamestate.attackcost.ToString();
            audioSource.PlayOneShot(gotUpgrade, 0.50f);
        }
    }
 
    public void OnFireRateUpgrade()
    {
        if(gamestate.fireratecost <= gamestate.currentCurreny)
        {
            float firerateAmoutIncreased = 0.08f;
            float fireratecostIncrease = 2f;

            gamestate.currentCurreny -= gamestate.fireratecost;
            gamestate.UpgradePlayerFireRate();
            gamestate.firerateIncreased += firerateAmoutIncreased;
            gamestate.fireratecost = (int)(gamestate.fireratecost * fireratecostIncrease);
            firerate.text = gamestate.fireratecost.ToString();
            audioSource.PlayOneShot(gotUpgrade, 0.50f);
        }
    }

    public void OnHealthUpgrade()
    {
        if(gamestate.maxhealthcost <= gamestate.currentCurreny)
        {
            int healthAmoutIncreased = 15;
            float maxhealthcostIncrease = 2f;

            gamestate.currentCurreny -= gamestate.maxhealthcost;
            gamestate.healthIncreased += healthAmoutIncreased ;
            gamestate.UpgradePlayerHealth(healthAmoutIncreased);
            gamestate.maxhealthcost = (int)(gamestate.maxhealthcost * maxhealthcostIncrease);
            maxhealth.text = gamestate.maxhealthcost.ToString();
            audioSource.PlayOneShot(gotUpgrade, 0.50f);
        }
    }
 
    public void OnHealthRegen()
    {
        if(gamestate.regencost <= gamestate.currentCurreny)
        {
            int healthAmoutRegen = 100;
            float regencostIncrease = 1.10f;

            gamestate.currentCurreny -= gamestate.regencost;
            gamestate.RegenPlayerAttack(healthAmoutRegen);
            gamestate.regencost = (int)(gamestate.regencost * regencostIncrease);
            regenhealth.text = gamestate.regencost.ToString();
            audioSource.PlayOneShot(gotUpgrade, 0.50f);
        }
    }
 }
