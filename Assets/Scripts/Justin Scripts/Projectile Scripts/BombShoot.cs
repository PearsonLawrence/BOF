using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject projectilePrefab;
    public GameObject explosionPrefab;
    public Transform bombPos;
    public float shootInterval = 2f;
    public float projectileSpeed = 5f;
    public float explosionDelay = 2f;
    public float explosionDuration = 1f;

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

            projRb.velocity = directionToPlayer * projectileSpeed;

            StartCoroutine(ExplodeAfterDelay(projectile));
        }
            
        IEnumerator ExplodeAfterDelay(GameObject projectile)
        {
            yield return new WaitForSeconds(explosionDelay);

            GameObject explosion = Instantiate(explosionPrefab, projectile.transform.position, Quaternion.identity);

            Destroy(projectile);

            StartCoroutine(MakeExplosionDisappear(explosion));
        }

        IEnumerator MakeExplosionDisappear(GameObject explosion)
        {
            yield return new WaitForSeconds(explosionDuration);

            Destroy(explosion);
        }

        
    }

    // Update is called once per frame
    
}
