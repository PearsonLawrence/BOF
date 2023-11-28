using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    

    public float currentHealth;

    [SerializeField] private GameObject deathParticlePrefab;

    [SerializeField]
    private bool isPlayerHealth;


    [SerializeField] private Image healthBar;
    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;

        if(isPlayerHealth && healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        if(currentHealth <= 0)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
