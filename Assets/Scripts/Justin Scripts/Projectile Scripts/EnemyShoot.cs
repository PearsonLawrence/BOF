using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    public float shootForce;
    [SerializeField] private FlyingEnemy mainBody;
    private FlyingEnemy mainBodyFlying;
    [SerializeField] private GameObject enemyPlayer;

    [SerializeField] EnemyType enemyType;

    [SerializeField] private float distanceToShoot; 
    private float timer;
    public bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
           mainBodyFlying = GetComponent<FlyingEnemy>();
           enemyPlayer = mainBodyFlying.Player;
             
    }
    private float currentDistToPlayer;
    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;


        currentDistToPlayer = Vector3.Distance(transform.position, enemyPlayer.transform.position);
               


        if (timer > 1.5 && currentDistToPlayer < distanceToShoot)
        {
            Vector3 rayDir = enemyPlayer.transform.position - projectilePos.position;

            RaycastHit2D PlayerAimRay = Physics2D.Raycast(projectilePos.position, rayDir, 10000);
            if (PlayerAimRay.collider.CompareTag("Player") || PlayerAimRay.collider.CompareTag("Shield"))
            {
                timer = 0;
                shoot();

            }
        }
    }

    void shoot()
    {
        GameObject temp = Instantiate(projectile, projectilePos.position, Quaternion.identity);
        temp.GetComponent<collisionDamageComponent>().owner = mainBodyFlying.GetHealthComponent();
        Rigidbody2D temp_rb = temp.GetComponent<Rigidbody2D>();

        Vector3 direction = enemyPlayer.transform.position - transform.position;
        temp.transform.up = direction;
        temp_rb.velocity = new Vector2(direction.x, direction.y).normalized * shootForce;
    }

}



