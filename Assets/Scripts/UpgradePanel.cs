using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public GameState gamestate;
    public GameObject panel;
    public GameObject points;
    public GameObject Upgrade1;
    public GameObject Upgrade2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //points.Text = ""+gamestate.currentCurreny;

     points.GetComponent<UnityEngine.UI.Text>().text = "points = "+gamestate.currentCurreny;


    }
}
