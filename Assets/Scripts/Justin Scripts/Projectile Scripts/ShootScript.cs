using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject projPrefab;
    public Transform shootPoint;

    public float shootingCooldown = 0.5f;
    private float shootingTimer =  0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= shootingTimer)
        {
            ShootProjectile();

            shootingTimer = Time.time + shootingCooldown;
        }
    }

    void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projPrefab, shootPoint.position, shootPoint.rotation);
    }
}
