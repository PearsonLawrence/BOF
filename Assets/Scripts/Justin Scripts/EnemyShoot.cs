using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1.5)
        {
            timer = 0;
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }
}
