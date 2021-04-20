using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    public GameObject elevator;
    public GameObject airconditioner;
    public GameObject[] possiblePositions;

    public GameObject[] shelfPositions;
    public GameObject[] counchPositions;
    public GameObject[] deskChairPositions;
    public GameObject[] chairPositions;
    public GameObject vendingMachinePosition;
    public GameObject couch;
    public GameObject shelf;
    public GameObject chair;
    public GameObject deskChair;
    public GameObject vendingMachine;
    // chance of spawning the furniture 1/spawnRate
    public int spawnRate;

    // chance of spawning the vending machine 1/spawnRate
    public int vendingMachineSpawnRate;


    private void Start()
    {
        int rand = Random.Range(0,5);
        if (rand == 0) SpawnAC();
        SpawnFurniture();
    }

    public void RoomDestruction()
    {
        Destroy(gameObject);
    }

    public void GenerateElevator()
    {
        int rand = Random.Range(0, possiblePositions.Length);
        Vector3 position = possiblePositions[rand].transform.position;
        GameObject acPosition = Instantiate(elevator, position, Quaternion.identity);
        GameObject toDestroy = possiblePositions[rand];
        possiblePositions[rand] = acPosition;
        Destroy(toDestroy);
    }

    public void SpawnAC()
    {
        int rand = Random.Range(0, possiblePositions.Length);
        Vector3 position = possiblePositions[rand].transform.position;
        GameObject acPosition = Instantiate(airconditioner, position, Quaternion.identity);
        GameObject toDestroy = possiblePositions[rand];
        possiblePositions[rand] = acPosition;
        Destroy(toDestroy);
    }

    public void SpawnFurniture()
    {
        for(int i = 0; i < shelfPositions.Length; i++)
        {
            int rand = Random.Range(0,spawnRate);
            if(rand == 0)
            {
                Instantiate(shelf, shelfPositions[i].transform.position, Quaternion.identity);
            }
            Destroy(shelfPositions[i]);
        }

        for (int i = 0; i < counchPositions.Length; i++)
        {
            int rand = Random.Range(0, spawnRate);
            if (rand == 0)
            {
                Instantiate(couch, counchPositions[i].transform.position, Quaternion.identity);
            }
            Destroy(counchPositions[i]);
        }

        for (int i = 0; i < deskChairPositions.Length; i++)
        {
            int rand = Random.Range(0, spawnRate);
            if (rand == 0)
            {
                Instantiate(deskChair, deskChairPositions[i].transform.position, Quaternion.identity);
            }
            Destroy(deskChairPositions[i]);
        }

        for (int i = 0; i < chairPositions.Length; i++)
        {
            int rand = Random.Range(0, spawnRate);
            if (rand == 0)
            {
                Instantiate(chair, chairPositions[i].transform.position, Quaternion.identity);
            }
            Destroy(chairPositions[i]);
        }

        int vendingMachineChance = Random.Range(0, vendingMachineSpawnRate);

        if (vendingMachineChance == 0)
        {
            Instantiate(vendingMachine, vendingMachinePosition.transform.position, Quaternion.identity);
        }

        Destroy(vendingMachinePosition);

    }
}
