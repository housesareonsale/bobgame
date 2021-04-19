using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] rooms; // 0 - LR | 1 - DLR | 2 - ULR | 3 - UDLR
    public LayerMask roomLayer;
    int[,] possibleStartPositions = new int[2, 2] { {5, 15}, {15, 15} };
    public float startTimeBetweenRoom = 0.25f;
    int direction; // 0,1 -> left | 2 -> down | 3,4 -> right
    public float moveAmount = 10; // this is the lenght and widht of the room
    public bool stopGeneration;
    public GameState gamestate;

    GameController gameController;
    GameObject lastestRoom;
    float timeBetweenRoom;
    int downCounter;

    void Start()
    {
        int randIndex = Random.Range(0, 2);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        transform.position = new Vector2(possibleStartPositions[randIndex,0], possibleStartPositions[randIndex,1]);
        lastestRoom = Instantiate(rooms[0], transform.position, Quaternion.identity);
        gamestate.playerLocation.position = transform.position;
        direction = Random.Range(0, 5);
    }

    void Update()
    {
        if(timeBetweenRoom <= 0 && !stopGeneration)
        {
            Move();
            timeBetweenRoom = startTimeBetweenRoom;
        }
        else
        {
            timeBetweenRoom -= Time.deltaTime;
        }
    }

    void Move()
    {
        if(direction == 0  || direction == 1) // MOVE RIGHT
        {
            if(transform.position.x <= (Util.GetRightXBound() - 15))
            {
                downCounter = 0;
                transform.position =  new Vector2(transform.position.x + moveAmount, transform.position.y);
                int rand = Random.Range(0, rooms.Length);
                lastestRoom = Instantiate( rooms[rand], transform.position, Quaternion.identity );

                direction = Random.Range(0, 3);
            }
            else
            {
                direction = 2;
            }
        }
        else if (direction == 2) // MOVE DOWN
        {
            downCounter++;

            if(transform.position.y >= (Util.GetDownYBound() + 15))
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);

                if (roomDetection && roomDetection.GetComponent<RoomType>() && roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    roomDetection.GetComponent<RoomType>().RoomDestruction();

                    if (downCounter >= 2)
                    {
                        lastestRoom = Instantiate( rooms[3], transform.position, Quaternion.identity );
                    }
                    else
                    {
                        int randomBottomRoom = Random.Range(1, 4);
                        if(randomBottomRoom == 2) randomBottomRoom = 1;
                        lastestRoom = Instantiate( rooms[randomBottomRoom], transform.position, Quaternion.identity );
                    }
                }

                transform.position =  new Vector2(transform.position.x, transform.position.y - moveAmount);
                int rand = Random.Range(2, 4);
                lastestRoom = Instantiate( rooms[rand], transform.position, Quaternion.identity );

                direction = Random.Range(0, 5);
            }
            else
            {
                gameController.LevelGenerationComplete(lastestRoom);
                stopGeneration = true;
                DestroyLevelGenerationObject();
            }
        }
        else if (direction == 3 || direction == 4) // MOVE LEFT
        {
            if(transform.position.x >= (Util.GetLeftXBound() + 15))
            {
                downCounter = 0;
                transform.position =  new Vector2(transform.position.x - moveAmount, transform.position.y);
                int rand = Random.Range(0, rooms.Length);
                lastestRoom = Instantiate( rooms[rand], transform.position, Quaternion.identity );

                direction = Random.Range(2, 5);
            }
            else 
            {
                direction = 2;
            }
        }
    }

    void DestroyLevelGenerationObject()
    {
        Destroy(gameObject);
    }
}
