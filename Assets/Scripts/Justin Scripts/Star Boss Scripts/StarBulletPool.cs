using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBulletPool : MonoBehaviour
{
    public static StarBulletPool bulletPoolInstance;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notEnoughInPool = true;

    private List<GameObject> bullets;

    private void Awake()
    {
        bulletPoolInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        if(bullets.Count > 0)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if(notEnoughInPool)
        {
            GameObject bull = Instantiate(pooledBullet);
            bull.SetActive(false);
            bullets.Add(bull);
            return bull;
        }
        return null;
    }
    
}
