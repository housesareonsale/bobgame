using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    public GameObject elevator;
    public GameObject[] possiblePositions;

    public void RoomDestruction()
    {
        Destroy(gameObject);
    }

    public void GenerateElevator()
    {
        int rand = Random.Range(0, possiblePositions.Length);
        Vector3 position = possiblePositions[rand].transform.position;
        Destroy(possiblePositions[rand]);
        Instantiate(elevator, position, Quaternion.identity);
        Debug.Log("Elevator has been generated");
    }

}
