using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    public float stoppingDistance = 1f;
    public float stopChase = 10f;
    public GameObject Player;
    public float circularMotionRadius = 2f;
    private float circularMotionAngle = 0f;
    public LayerMask obstacleLayer;
    public float avoidanceDistance = 1f;
    [SerializeField] private HealthComponent HC;

    Ray ray;
    

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        startingPoint = transform;
        HC = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
            return;
        float distanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if(distanceToPlayer >= stoppingDistance && distanceToPlayer <= stopChase)
        {
            chase = true; 
            Vector2 direction = -(transform.position - (Vector3)Player.transform.position).normalized;
            transform.up = direction;
        }
        else if(distanceToPlayer <= stopChase)
        {
            chase = false;
            Vector2 direction = -(transform.position - (Vector3)Player.transform.position).normalized;
            transform.up = direction;
        }
        else
        {
            ReturnToStartPoint();
            chase = false;
        }

        if (chase)
            Chase();
       
            

    }
    public HealthComponent GetHealthComponent()
    {
        return HC;
    }
    private void Chase()
    {

        

        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, avoidanceDistance, obstacleLayer);
            if (hit.collider != null)
            {
                // Avoid obstacle by changing direction (turn 90 degrees)
                transform.Rotate(Vector3.forward, 90f);
            }
    }

    private void ReturnToStartPoint()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
         

    }

    
}
