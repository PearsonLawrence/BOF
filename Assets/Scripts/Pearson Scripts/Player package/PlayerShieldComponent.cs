using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldComponent : MonoBehaviour
{

    [SerializeField] private HealthComponent playerHC;
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private int maxBulletCollection;
    [SerializeField] private int shieldBulletDamage;
    [SerializeField] private int shieldDestroyDamage;
    [SerializeField] private float bulletLaunchVel;
    private float currentTotalBulletCollectionCount;

    public void addBulletToCollection(EnemyType bulletType)
    {
        currentTotalBulletCollectionCount++;
        
        if (currentTotalBulletCollectionCount >= maxBulletCollection)
            destroyShield();
    }

    void destroyShield()
    {
        playerHC.takeDamage(shieldDestroyDamage);
        currentTotalBulletCollectionCount = 0;
        this.gameObject.SetActive(false);
    }

    public void shootProjectile()
    {
        GameObject temp = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
        temp.GetComponent<collisionDamageComponent>().owner = playerHC;
        Rigidbody2D temp_rb = temp.GetComponent<Rigidbody2D>();

        temp_rb.velocity = transform.right * bulletLaunchVel;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
