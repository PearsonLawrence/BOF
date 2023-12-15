using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class RandomAbilityEnemy : MonoBehaviour
{
    [Header("Swap Positions")]
    public float swapDistance = 5f;
    
    [Header("Damage Pulse")]
    public float pulseRadius = 3f;
    public float pulseDamage = 5f;
    public Sprite damagePulse;
    public float knockbackForce = 10f;

    [Header("Duplicate")]
    public GameObject projectilePrefab;
    public Transform projSpawnPoint;
    public float projForce = 10f;
    public float duplicateDuration = 5f;
    public float currentDuplicateDuration = 5f;
    public float duplicateStoppingDistance = 2f;
    public float timeBetweenProjectiles = 2f;
    private float currentTimeBetweenProjectiles;

    [Header("Enemy Movement")]
    public float speed = 3f;
    public float distanceBetween = 3f;
    private float distance;
    public GameObject player;
    public float minX = 0.5f;
    public float maxX = 1f;
    public float minY = 0.5f;
    public float maxY = 1f;

    public float maxRandAbilitytimer;
    public float randAbilityTimer;

    public float isActiveDistance = 20;

    public GameObject duplicatePrefab;
    public Animator anim;
    public RandomAbilityEnemy duplicate;
    public bool isDupe;

    public ParticleSystem explodePulse;
    public SpriteRenderer render1, render2;
    public float activeDistance;
    private HealthComponent HC;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        HC = GetComponent<HealthComponent>();
        if (!isDupe)
        {
            duplicate = Instantiate(duplicatePrefab, transform.position, Quaternion.identity).GetComponent<RandomAbilityEnemy>();
            duplicate.gameObject.SetActive(false);
            duplicate.isDupe = true;
            duplicate.GetComponent<HealthComponent>().canDropPowerup = false;
            duplicate.GetComponent<HealthComponent>().isDisableOnDeath = true;
        }

        minX = transform.position.x;
        maxX = transform.position.x + 0.5f;
        minY = transform.position.x;
        maxY = transform.position.x + 0.5f;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance > isActiveDistance)
        {
            if (render1.enabled)
            {
                render1.enabled = false;
                render2.gameObject.SetActive(false);
            }
            return;
        }
        else
        {
            if (!render1.enabled)
            {
                render1.enabled = true;
                render2.gameObject.SetActive(true);
            }
        }


        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        


        if(isDupe)
        {
            if (distance < (distanceBetween*2))
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            if (currentTimeBetweenProjectiles >= -0.1f) currentTimeBetweenProjectiles -= Time.deltaTime;

           
            if(currentTimeBetweenProjectiles < 0)
            {
                doShootAttack();
                currentTimeBetweenProjectiles = timeBetweenProjectiles;
            }
            
        }
        else
        {
            if (distance < (distanceBetween))
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            if (currentDuplicateDuration >= -0.1f) currentDuplicateDuration -= Time.deltaTime;

            if (currentDuplicateDuration < 0)
            {
                
                duplicate.gameObject.SetActive(false);
            }

            if (randAbilityTimer >= -.1f) randAbilityTimer -= Time.deltaTime;

            if(randAbilityTimer < 0 && !isRandom)
            {

                doRandom();
            }
        }
    
    }

    void RandomAbility()
    {
        if (isDupe) return;

        int randomAbility = Random.Range(0, 30);
        Debug.Log("Case " + randomAbility);

        if(randomAbility <= 10)
        {
            SwapWithPlayer();
        }
        else if(randomAbility <= 20)
        {
            DamagingPulse();
        }
        else if(randomAbility <= 30)
        {
            CreateDuplicate();
        }
              

    }

    void SwapWithPlayer()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < swapDistance)
        {
            Vector2 playerPos = player.transform.position;
            player.transform.position = transform.position;
            transform.position = playerPos;
        }
    }

    void DamagingPulse()
    {
        // need to apply damage to player
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pulseRadius);
        explodePulse.Play();
        foreach (Collider2D collider in colliders)
        {
            if(collider.CompareTag("Player"))
            {
               HealthComponent tempHC = collider.GetComponent<HealthComponent>();
                tempHC.takeDamage(pulseDamage);

                Rigidbody2D tempRB = tempHC.getRBRef();
                if (tempRB != null)
                {
                    Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
                    tempRB.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    void CreateDuplicate()
    {

        duplicate.transform.position = transform.position;
        duplicate.gameObject.SetActive(true);
        currentDuplicateDuration = duplicateDuration;
        
    }


    public bool isAttacking;
    public bool isRandom;
    public GameObject shooter;
    
    public void endRandom()
    {
        isRandom = false;
        anim.SetBool("isDoRandom", isRandom);
        randAbilityTimer = maxRandAbilitytimer;
    }

    public void doRandom()
    {
        isRandom = true;
        anim.SetBool("isDoRandom", isRandom);
    }
    public void doShootAttack()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", isAttacking);
        
    }
    public void EndShoot()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        currentTimeBetweenProjectiles = timeBetweenProjectiles;
    }
    public void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        collisionDamageComponent tempCol = projectile.GetComponent<collisionDamageComponent>();
        tempCol.owner = HC;
        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
        projRb.velocity = (player.transform.position - projectile.transform.position).normalized * projForce;
        
    }

}
