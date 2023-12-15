using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{

    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private float timer;

    public float repeatRate = 1f;

    [SerializeField] float launchSpeed;

    private Vector2 bulletMoveDirection;

    private HealthComponent health;
    [SerializeField] private StarBoss boss;
    [SerializeField] private GameObject sfxPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        health = GetComponent<HealthComponent>();
        boss = GetComponent<StarBoss>();
    }
    public void StartFireRepetition(float rate)
    {
        InvokeRepeating("doFire", 0f, rate);
    }
    private void doFire()
    {
        boss.anim.SetBool("isShooting", true);
    }
    public void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;


            if (sfxPrefab != null) Instantiate(sfxPrefab, transform.position, Quaternion.identity);

            GameObject bul = StarBulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.SetActive(true);
            bul.GetComponent<Rigidbody2D>().velocity = (bulDir * launchSpeed);
            bul.transform.up = bulDir;
            bul.GetComponent<collisionDamageComponent>().owner = health;
            angle += angleStep;
        }
        boss.anim.SetBool("isShooting", false);
    }

    
    
       
    

}
