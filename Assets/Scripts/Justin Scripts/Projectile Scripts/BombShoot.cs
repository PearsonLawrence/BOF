using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;
    public Transform bombPos;
    public float shootInterval = 7f;
    public float projectileSpeed = 3f;
    public float explosionDelay = 3f;
    public float explosionDuration = 1f;
    public float projGravity = 5f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.LogError("Player not found in the scene.");
        }
        
        StartCoroutine(ShootRoutine());    
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            ShootProjectile();
            yield return new WaitForSeconds(shootInterval);
        }

        void ShootProjectile()
        {
            if (player == null)
            {
                return;
            }

            Vector3 spawnPos = bombPos.TransformPoint(Vector3.zero);
            GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
            
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
            float arcAngle = angle + Mathf.Deg2Rad * 60f;

            // Apply an initial velocity
            float speedX = Mathf.Cos(arcAngle) * projectileSpeed;
            float speedY = Mathf.Sin(arcAngle) * projectileSpeed;
            projRb.velocity = new Vector2(speedX, speedY);
 
            projRb.gravityScale = projGravity;

            StartCoroutine(ExplodeAfterDelay(projectile));

        }

        IEnumerator ExplodeAfterDelay(GameObject projectile)
        {
            yield return new WaitForSeconds(explosionDelay);

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(projectile); // Destroy the projectile

            StartCoroutine(MakeExplosionDisappear(explosion));
        }

        IEnumerator MakeExplosionDisappear(GameObject explosion)
        {
            yield return new WaitForSeconds(explosionDuration);

            Destroy(explosion);
        }


    }

}
