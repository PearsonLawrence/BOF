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
    public bool canDropPowerup;


    [SerializeField] private Image healthBar;

    [SerializeField] private bool hasScore;
    [SerializeField] private int scoreValue;


    [SerializeField] private AudioSource damageSFX;

    [SerializeField] private Rigidbody2D RBref;
    public float spawnRadius = 1.5f;
    private PlayerScoreComponent score;

    public Clock clock;
    public GameManagerComponent manager;

    [SerializeField] private GameObject FluxionPartPrefab;

    public bool isDisableOnDeath;
    public bool isCrystal;
    public bool isCrystalBoss;
    public bool isStarBoss;
    public DestroyableObject crystal;
    public AudioManager audioman;
    public GameObject audioPrefab;
    public void Start()
    {
        currentHealth = maxHealth;
        camShake = Camera.main.GetComponent<CameraShake>();
        if(hasScore)
        {
            score = Camera.main.GetComponent<PlayerScoreComponent>();
        }

        if (isCrystalBoss)
        {
            clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<Clock>();
            if (clock)
            {
                clock.updateTimeTrialInfo(this.gameObject);
                manager = clock.gameObject.GetComponent<GameManagerComponent>();
            }
        }
        if (isStarBoss)
        {
            manager = GameObject.FindGameObjectWithTag("Clock").GetComponent<GameManagerComponent>();
            audioman = Camera.main.GetComponent<AudioManager>();
        }

        if (isPlayerHealth)
        {
            manager = GameObject.FindGameObjectWithTag("Clock").GetComponent<GameManagerComponent>();
           
        }
    }
    public void DestroyBoss()
    {

        if (deathParticlePrefab != null) Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        manager.starDeath();

        this.gameObject.SetActive(false);
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (isPlayerHealth && healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }
    public void EndDamageAnimation()
    {
        
        damageAnimator.SetBool("isTakeDamage", false);
       
    }
    private bool bossDestroy;
    public void takeDamage(float amount)
    {
        if(!canTakeDamage || bossDestroy) return;

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
           
            if (dropPrefab != null && rand > 50)
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
            if (isStarBoss)
            {
                if (audioPrefab != null) Instantiate(audioPrefab, transform.position, Quaternion.identity);

                audioman.bossDead = true;

                damageAnimator.SetBool("isDestroyBoss", true);
                bossDestroy = true;
                return;
            }
            if (deathParticlePrefab != null && !isStarBoss) Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

            if(canDropPowerup && dropPrefab != null)
                Instantiate(dropPrefab, transform.position, Quaternion.identity);

            if (camShake != null)
            {
                ShakeType type = (isPlayerHealth) ? ShakeType.playerDeath : ShakeType.enemyDeath;
                camShake.doShake(type);
            }


            if(isCrystalBoss)
            {
                clock.removeBoss(this.gameObject);
                manager.updateScore();
            }

            if(isPlayerHealth)
            {
                Debug.Log("PlayerDeath");
                manager.playerDeath();
            }

            if(!isDisableOnDeath)
                Destroy(this.gameObject);
            else
            {
                if(isCrystal)
                {
                    crystal.doDestroy();
                }
                else
                    currentHealth = maxHealth;


                this.gameObject.SetActive(false);

            }
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
