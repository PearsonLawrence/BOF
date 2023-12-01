using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    public float shootForce;
    [SerializeField] private TankEnemy mainBodyTank;
    private FlyingEnemy mainBodyFlying;
    [SerializeField] private GameObject enemyPlayer;

    [SerializeField] EnemyType enemyType;

    [SerializeField] private float distanceToShoot; 
    private float timer;
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
        timer += Time.deltaTime;


        currentDistToPlayer = Vector3.Distance(transform.position, enemyPlayer.transform.position);
               


        if (timer > 1.5 && currentDistToPlayer < distanceToShoot)
        {
            Vector3 rayDir = enemyPlayer.transform.position - projectilePos.position;

            RaycastHit2D PlayerAimRay = Physics2D.Raycast(projectilePos.position, rayDir, 10000);
            if (PlayerAimRay.collider.CompareTag("Player"))
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
        temp_rb.velocity = new Vector2(direction.x, direction.y).normalized * shootForce;
    }

}



