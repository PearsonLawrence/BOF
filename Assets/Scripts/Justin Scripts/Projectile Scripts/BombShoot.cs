using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;
    public HealthComponent HC;
    public Transform bombPos;
    public float shootInterval = 7f;
    public float shootDistance = 8f;
    private float currentShootInterval = 7f;
    public float projectileSpeed = 3f;
    public float explosionDelay = 3f;
    public float explosionDuration = 1f;
    public float projGravity = 5f;
    public float ActiveDistance = 30f;
    public bool isActive = true;
    private GameObject player;
    public Animator anim;
    private HealthComponent tempHCowner; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.LogError("Player not found in the scene.");
        }
        
    }

    public void Update()
    {
        float DistToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (DistToPlayer > ActiveDistance)
        {
            isActive = false;
            return;
        }
        else
        {
            isActive = true;
        }
        currentShootInterval -= Time.deltaTime;

        if (DistToPlayer > shootDistance) return;
        if (currentShootInterval < 0) doShoot();

    }

    public void endShoot()
    {
        currentShootInterval = shootInterval;
        anim.SetBool("isAttacking", false);
    }

    public void doShoot()
    {
        anim.SetBool("isAttacking", true);
    }

    public void ShootProjectile()
    {
        Debug.Log("Shoot bomb1");
        if (player == null) return;
        Debug.Log("Shoot bomb2");


        Vector3 spawnPos = bombPos.TransformPoint(Vector3.zero);
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
        collisionDamageComponent tempCol = projectile.GetComponent<collisionDamageComponent>();
        tempCol.owner = HC;
        Debug.Log(tempCol.owner);

        Vector2 directionToPlayer = (player.transform.position - transform.position);
        
        projRb.velocity = directionToPlayer * projectileSpeed;
        projectile.transform.up = directionToPlayer;

        projRb.gravityScale = projGravity;

        Debug.Log("Shoot bomb3");
        StartCoroutine(ExplodeAfterDelay(projectile));

    }


    IEnumerator ExplodeAfterDelay(GameObject projectile)
    {
        yield return new WaitForSeconds(explosionDelay);

        GameObject explosion = Instantiate(explosionPrefab, projectile.transform.position, Quaternion.identity);
        collisionDamageComponent tempCol = explosion.GetComponent<collisionDamageComponent>();
        tempCol.owner = HC;
        Debug.Log(tempCol.owner);
        Destroy(projectile); // Destroy the projectile
        Destroy(explosion, explosionDuration); // Destroy the projectile

    }


}
