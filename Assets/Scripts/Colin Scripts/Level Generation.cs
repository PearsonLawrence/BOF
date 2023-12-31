using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRBT
    public Transform CurrentRoom;
    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    public bool stopGeneration;

    public LayerMask room;

    private int downCounter;
    private bool isFinalRoomCreated = false;
    public GameObject FirstRoom;
    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        GameObject tempRoom = Instantiate(rooms[4], transform.position, Quaternion.identity);
        CurrentRoom = tempRoom.transform;
        FirstRoom = CurrentRoom.gameObject;

        direction = Random.Range(1, 6);
    }

    private void Move()
    {
        if (direction == 1 || direction == 2)
        { //move right
            if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, 3);
                if (rand == 4) rand = Random.Range(0, 3);

                GameObject tempRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                CurrentRoom = tempRoom.transform;
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { //move left
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, 3);
                if (rand == 4) rand = Random.Range(0, 3);

                GameObject tempRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                CurrentRoom = tempRoom.transform;
                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5)
        { //move down

            downCounter++;

            if (transform.position.y > minY)
            {

                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                    {

                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        GameObject tempRoom1 = Instantiate(rooms[3], transform.position, Quaternion.identity);
                        CurrentRoom = tempRoom1.transform;
                    }
                    else
                    {

                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 3);
                        if(randBottomRoom == 4) randBottomRoom = Random.Range(1, 3);

                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        GameObject tempRoom2 = Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        CurrentRoom = tempRoom2.transform;
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 3);
                GameObject tempRoom3 = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                CurrentRoom = tempRoom3.transform;
                direction = Random.Range(1, 6);
            }
            else //stop level generation
            {
                if(!isFinalRoomCreated)
                {
                    Transform tempPos = CurrentRoom.transform;
                    Destroy(CurrentRoom.gameObject);
                    GameObject tempRoom4 = Instantiate(rooms[5], tempPos.position, Quaternion.identity);
                    CurrentRoom = tempRoom4.transform;
                    isFinalRoomCreated = true;
                }
                stopGeneration = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }
}
