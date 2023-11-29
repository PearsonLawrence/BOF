using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBoss : MonoBehaviour
{

    public float moveSpeed = 5f;
    private bool moveRight;
    public float rotationSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        moveRight = true;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 7f)
        {
            moveRight = false;
        }
        
        else if(transform.position.x < -7f)
        {
            moveRight = true;
        }

        if(moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);  
        }

        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
