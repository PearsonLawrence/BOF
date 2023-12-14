using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
    public float speed = 5f;
    public float triangleAmplitude = 2f;
    private Vector2 initialPosition;
    private float timeElapsed = 0f;

    private Transform playerTransform;


  

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
        InvokeRepeating("RandomAbility", 2f, 5f);
    }

    private void Update()
    {
        //movement
        
        /*timeElapsed += Time.deltaTime;
        float xOffset = Mathf.Sin(timeElapsed * speed) * triangleAmplitude;
        float yOffset = Mathf.Cos(timeElapsed * speed) * triangleAmplitude;
        transform.position = initialPosition + new Vector2(xOffset, 0f); */
    }

    void RandomAbility()
    {
        int randomAbility = Random.Range(0, 3);
        Debug.Log("Case " + randomAbility);

        switch (randomAbility)
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
        Destroy(duplicate.GetComponent<RandomAbilityEnemy>());
        Destroy(duplicate, duplicateDuration);

        Vector2 directionToPlayer = (playerTransform.position - duplicate.transform.position).normalized;
        Vector2 stoppingPoint = (Vector2)playerTransform.position - directionToPlayer * duplicateStoppingDistance;
        duplicate.transform.position = stoppingPoint;
        
        Rigidbody2D duplicateRb = duplicate.GetComponent<Rigidbody2D>();
        duplicateRb.velocity = (playerTransform.position - duplicate.transform.position).normalized;

        StartCoroutine(ShootProjectiles(duplicate));
    }

    IEnumerator ShootProjectiles(GameObject duplicate)
    {
        while(true)
        {
            GameObject projectile = Instantiate(projectilePrefab, projSpawnPoint.position, Quaternion.identity);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
            projRb.velocity = (playerTransform.position - projSpawnPoint.position).normalized * projForce;

            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
    }
}
