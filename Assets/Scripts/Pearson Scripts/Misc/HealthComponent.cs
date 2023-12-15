using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
  
    public float currentHealth;
    public bool canTakeDamage = true;

    [SerializeField] private CameraShake camShake;

    [SerializeField] private Animator damageAnimator;

    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private GameObject dropPrefab;

    [SerializeField] private bool isPlayerHealth;
    [SerializeField] private bool canDropPowerup;


    [SerializeField] private Image healthBar;

    [SerializeField] private bool hasScore;
    [SerializeField] private int scoreValue;


    [SerializeField] private AudioSource damageSFX;

    [SerializeField] private Rigidbody2D RBref;
    public float spawnRadius = 1.5f;
    private PlayerScoreComponent score;

    [SerializeField] private GameObject FluxionPartPrefab;
    public void Start()
    {
        currentHealth = maxHealth;
        camShake = Camera.main.GetComponent<CameraShake>();
        if(hasScore)
        {
            score = Camera.main.GetComponent<PlayerScoreComponent>();
        }

    }
    public Rigidbody2D getRBRef()
    {
        return RBref;
    }

    public void setDamageAnimator(Animator animator)
    {
        damageAnimator = animator;
    }
    public void DoHeal(float amount)
    {
        currentHealth += amount;
        if (isPlayerHealth && healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }
    public void EndDamageAnimation()
    {
        
        damageAnimator.SetBool("isTakeDamage", false);
       
    }
    public void takeDamage(float amount)
    {
        if(!canTakeDamage) return;

        if (damageSFX != null) damageSFX.Play();
        currentHealth -= amount;

        if(camShake != null && currentHealth > 0)
        {
            ShakeType type = (isPlayerHealth) ? ShakeType.playerDamage : ShakeType.enemyDamage;
            camShake.doShake(type);
        }

        if(damageAnimator != null)
        {
            damageAnimator.SetBool("isTakeDamage", true);
        }

        if(isPlayerHealth && healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
            int rand = Random.Range(1, 100);
           
            if (dropPrefab != null && rand > 30)
            {
                float radius = spawnRadius;
                Vector3 randomPos = Random.onUnitSphere * radius;
                randomPos += transform.position;
                randomPos.z = 0f;

                Vector3 direction = randomPos - transform.position;

                GameObject temp = Instantiate(dropPrefab, randomPos, Quaternion.identity);

                Rigidbody2D tempRB = temp.GetComponent<Rigidbody2D>();
                
                tempRB.velocity = direction * Random.Range(5,10);
                temp.transform.up = direction;
            }
        }
        if(currentHealth <= 0 )
        {
            if (hasScore)
            {
                if(score)
                {
                    score.increaseScore(scoreValue);
                }
            }

            if (deathParticlePrefab != null) Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

            if(canDropPowerup && dropPrefab != null)
                Instantiate(dropPrefab, transform.position, Quaternion.identity);

            if (camShake != null)
            {
                ShakeType type = (isPlayerHealth) ? ShakeType.playerDeath : ShakeType.enemyDeath;
                camShake.doShake(type);
            }
            Destroy(this.gameObject);
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
