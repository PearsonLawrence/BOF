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
    private PlayerScoreComponent score;
    public void Start()
    {
        currentHealth = maxHealth;
        camShake = Camera.main.GetComponent<CameraShake>();
        if(hasScore)
        {
            score = Camera.main.GetComponent<PlayerScoreComponent>();
        }

    }

    public void setDamageAnimator(Animator animator)
    {
        damageAnimator = animator;
    }

    public void EndDamageAnimation()
    {
        
        damageAnimator.SetBool("isTakeDamage", false);
       
    }
    public void takeDamage(float amount)
    {
        if(!canTakeDamage) return;

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

            if(dropPrefab != null) Instantiate(dropPrefab, transform.position, Quaternion.identity);
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
