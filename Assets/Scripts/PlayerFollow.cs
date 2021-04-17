using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform follow;

    void Update()
    {
        Vector3 position = follow.localPosition;
        this.transform.localPosition = new Vector3(position.x, position.y, -10f);       
    }
}
