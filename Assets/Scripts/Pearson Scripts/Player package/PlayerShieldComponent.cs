using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldComponent : MonoBehaviour
{

    [SerializeField] private PlayerCombatComponent playerCombat;
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private int maxBulletCollection;
    [SerializeField] private int shieldBulletDamage;
    [SerializeField] private int buffedShieldBulletDamage;
    [SerializeField] private int shieldDestroyDamageBase;
    private int shieldDestroyDamage;
    [SerializeField] private float bulletLaunchVel;
    [SerializeField] private float bulletSizeModifier;
    private int currentTotalBulletCollectionCount;
    [SerializeField] private ParticleSystem ShieldDestroy;
    public float ShieldCooldown;
    public float ShieldDestroyCooldown;
    private float shieldCooldownTimer;
    public bool isBlock;
    public bool isPoweredUp;
    public void addBulletToCollection()
    {
        currentTotalBulletCollectionCount++;
        
        if (currentTotalBulletCollectionCount >= maxBulletCollection)
            destroyShield();
    }

    public int CalcShieldDamage()
    {
        return shieldDestroyDamageBase * maxBulletCollection;
    }

    public void destroyShield()
    {
        if(ShieldDestroy) ShieldDestroy.Play();

        playerCombat.GetHealthComponent().takeDamage(shieldDestroyDamageBase);
        currentTotalBulletCollectionCount = 0;
        isBlock = false;
        playerCombat.anim.SetBool("isBlocking", false);
        shieldCooldownTimer = ShieldDestroyCooldown;
        this.gameObject.SetActive(false);
    }

    public float GetShieldCoolDownTimer()
    {
        return shieldCooldownTimer;
    }

    public float GetShieldCoolDown()
    {
        return ShieldCooldown;
    }
    public void UpdateShieldCooldownTimer()
    {

        shieldCooldownTimer -= Time.deltaTime;
    }
    public void ResetShieldCooldown()
    {
        shieldCooldownTimer = ShieldCooldown;
    }

    public void shootProjectile()
    {
        if (currentTotalBulletCollectionCount <= 0) return;


        float scaleModify = currentTotalBulletCollectionCount * bulletSizeModifier;

        GameObject temp = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
        collisionDamageComponent collisionComp = temp.GetComponent<collisionDamageComponent>();


        collisionComp.owner = playerCombat.GetHealthComponent();


        
        

        temp.transform.localScale = new Vector3(temp.transform.localScale.x + scaleModify, temp.transform.localScale.y + scaleModify, temp.transform.localScale.z);
        Rigidbody2D temp_rb = temp.GetComponent<Rigidbody2D>();
        temp.transform.up = transform.right;
        temp_rb.velocity = transform.right * bulletLaunchVel;

        if (isPoweredUp)
        {
            collisionComp.currentDamage = buffedShieldBulletDamage * currentTotalBulletCollectionCount;
            collisionComp.isDestroyedOnHit = false;
            currentTotalBulletCollectionCount = 0;
            Destroy(temp, 5);
        }
        else
        {
            collisionComp.currentDamage = shieldBulletDamage * currentTotalBulletCollectionCount;
            currentTotalBulletCollectionCount = 0;
        }


    }
    public void AddMaxBulletCollection(int amount)
    {
        maxBulletCollection += amount;
    }

    public void AddBulletCollection(int amount)
    {
        currentTotalBulletCollectionCount += amount;
    }
    public void SetMaxBulletCollection(int count)
    {
        maxBulletCollection = count;
    }

    public void SetBulletCollection(int count)
    {
        currentTotalBulletCollectionCount = count;
    }
    public int GetMaxBulletCollection()
    {
        return maxBulletCollection;
    }

    public int GetBulletCollection()
    {
        return currentTotalBulletCollectionCount;
    }

    void Start()
    {
        
    }

}
