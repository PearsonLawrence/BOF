using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomAbilityEnemy : MonoBehaviour
{
    [Header("Swap Positions")]
    public float swapDistance = 5f;
    
    [Header("Damage Pulse")]
    public float pulseRadius = 3f;
    public Sprite damagePulse;
    
    [Header("Duplicate")]
    public GameObject projectilePrefab;
    public Transform projSpawnPoint;
    public float projForce = 10f;
    public float duplicateDuration = 5f;
    public float duplicateStoppingDistance = 2f;
    public float timeBetweenProjectiles = 2f;

    [Header("Enemy Movement")]
    public float speed = 3f;
    public float distanceBetween = 3f;
    private float distance;
    public GameObject player;
    public float min = 0.5f;
    public float max = 1f;

    private Transform playerTransform;


  

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("RandomAbility", 2f, 5f);

        min = transform.position.x;
        max = transform.position.x + 0.5f;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        if(distance > distanceBetween)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
        }
    
    }

    void RandomAbility()
    {
        int randomAbility = Random.Range(0, 3);
        Debug.Log("Case " + randomAbility);

        switch (2)
        {
            case 0:
                SwapWithPlayer();
                break;

            case 1:
                DamagingPulse();
                break;

            case 2:
                CreateDuplicate();
                break;
        }

    }

    void SwapWithPlayer()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) < swapDistance)
        {
            Vector2 playerPos = playerTransform.position;
            playerTransform.position = transform.position;
            transform.position = playerPos;
        }
    }

    void DamagingPulse()
    {

        GameObject pulseVisual = new GameObject("DamagingPulseVisual");
        pulseVisual.transform.position = transform.position;
        SpriteRenderer pulseRenderer = pulseVisual.AddComponent<SpriteRenderer>();
        pulseRenderer.color = new Color(1f, 0f, 0f, 0.5f);

        pulseRenderer.sprite = damagePulse;
        
        float pulseSize = pulseRadius * 2f;
        pulseVisual.transform.localScale = new Vector3(pulseSize, pulseSize, 1f);

       
        //start smaller and grow bigger as it comes out
        /*float initSize = 0.1f;
        pulseVisual.transform.localScale = new Vector3(initSize, initSize, 1f); */

        Destroy(pulseVisual, 0.5f);
        
        // need to apply damage to player
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pulseRadius);

        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Player"))
            {
                Debug.Log("Player damaged!");
            }
        }
    }

    void CreateDuplicate()
    {
        
        GameObject duplicate = Instantiate(gameObject, transform.position, transform.rotation);
        Debug.Log("Created duplicate");
        duplicate.GetComponent<RandomAbilityEnemy>().enabled = false;
        Destroy(duplicate, duplicateDuration);
        

        if (duplicate != null)
        {
            Vector2 directionToPlayer = (playerTransform.position - duplicate.transform.position).normalized;
            Vector2 stoppingPoint = (Vector2)playerTransform.position - directionToPlayer * duplicateStoppingDistance;
            duplicate.transform.position = stoppingPoint;

            Rigidbody2D duplicateRb = duplicate.GetComponent<Rigidbody2D>();
            duplicateRb.velocity = (playerTransform.position - duplicate.transform.position).normalized;

            StartCoroutine(ShootProjectiles(duplicate));

        }
        
    }

    IEnumerator ShootProjectiles(GameObject shooter)
    {
        while(true)
        {
            GameObject projectile = Instantiate(projectilePrefab, shooter.transform.position, Quaternion.identity);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
            projRb.velocity = (playerTransform.position - projectile.transform.position).normalized * projForce;

            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
    }
}
