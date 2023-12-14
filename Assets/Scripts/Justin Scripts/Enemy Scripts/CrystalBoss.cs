using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBoss : MonoBehaviour
{
    public int objectsToDestroy = 4;
    private int destroyedObjects = 0;
    public float hitRadius = 0.1f;

    public GameObject hands;
  
    // Start is called before the first frame update
    void Start()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hitRadius);

        foreach(Collider2D collider in colliders)
        {
            DestroyableObject destObj = collider.GetComponent<DestroyableObject>();

            if(destObj != null)
            {
                hands.GetComponent<collisionDamageComponent>().enabled = true;
            }
            else
            {
                hands.GetComponent<collisionDamageComponent>().enabled = false;
            }
        }
        
        
        
        
        
        /*if (objectsToDestroy == 0)
        {
            GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach(GameObject obj in allGameObjects)
            {
                if(obj.GetComponent<DestroyableObject>() != null)
                {
                    hands.GetComponent<PlayerCombatComponent>().enabled = true;
                }
            }
            
        }
        else
        {
            
            hands.GetComponent<PlayerCombatComponent>().enabled = false;
            InitializeObjects();
        } */

        InitializeObjects();
    }

    private void InitializeObjects()
    {
        DestroyableObject[] destroyableObjects = FindObjectsOfType<DestroyableObject>();

        foreach(var obj in destroyableObjects)
        {
            obj.onDestroy += ObjectDestroyed;
        }
    }

    private void ObjectDestroyed()
    {
        destroyedObjects++;
        if(destroyedObjects >= objectsToDestroy)
        {
            hands.GetComponent<PlayerCombatComponent>().enabled = true;
        }
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
