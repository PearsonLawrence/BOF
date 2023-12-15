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
    public float ActiveDistance = 30;
    [SerializeField] private HealthComponent HC;
    [SerializeField] private SpriteRenderer renderItem1, renderItem2;
    [SerializeField] private EnemyShoot shootComp;


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

        if (distanceToPlayer >= ActiveDistance)
        {

            if (shootComp.isActive == true)
            {
                shootComp.isActive = false;
                renderItem1.enabled = false;
                renderItem2.enabled = false;
            }
            return;
        }
        else
        {
            if(shootComp.isActive == false)
            {
                shootComp.isActive = true;
                renderItem1.enabled = true;
                renderItem2.enabled = true;
            }
        }

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
        Vector3 rayDir = Player.transform.position - transform.position;

        RaycastHit2D PlayerAimRay = Physics2D.Raycast(transform.position, rayDir, avoidanceDistance, obstacleLayer);


        if(!PlayerAimRay)
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    private void ReturnToStartPoint()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
         

    }

    
}
