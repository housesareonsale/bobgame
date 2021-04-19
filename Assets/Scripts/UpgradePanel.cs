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
    public int attackcost;
    public int fireratecost;

    void Update()
    {
        points.text = gamestate.currentCurreny + " points";
    }

    public void OnAttackUpgrade()
    {
        if(attackcost <= gamestate.currentCurreny)
        {
            gamestate.currentCurreny -= attackcost;
            gamestate.UpgradePlayerAttack();
            attackcost *= 2;
            attack.text = "damage\n" + attackcost.ToString();
        }
    }
    public void OnFireRateUpgrade()
    {
        if(fireratecost <= gamestate.currentCurreny)
        {
            gamestate.currentCurreny -= fireratecost;
            gamestate.UpgradePlayerFireRate();
            fireratecost *= 2;
            firerate.text = "firerate\n" + fireratecost.ToString();
        }
    }
}
