using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    public float stoppingDistance = 1f;
    private GameObject player;
    public float circularMotionRadius = 2f;
    private float circularMotionAngle = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if(distanceToPlayer <= stoppingDistance)
        {
            chase = false;
        }
        else
        {
            chase = true;
        }
        if (chase)
            Chase();
        else
        {
            ReturnToStartPoint();
        }
        Flip();
            

    }

    private void Chase()
    { 
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    private void ReturnToStartPoint()
    {
        circularMotionAngle += speed * Time.deltaTime;

        float x = startingPoint.position.x + Mathf.Cos(circularMotionAngle);
        float y = startingPoint.position.y + Mathf.Sin(circularMotionAngle);

        Vector2 circularMotionPosition = new Vector2(x, y);
        
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed*Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
