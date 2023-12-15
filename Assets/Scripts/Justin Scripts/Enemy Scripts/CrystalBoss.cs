using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBoss : MonoBehaviour
{
    public int objectsToDestroy = 4;
    private int destroyedObjects = 0;

    public HealthComponent hc;
  
    // Start is called before the first frame update
    void Start()
    {
        hc.canTakeDamage = false;
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
        Debug.Log("Destroyed Objects: " + destroyedObjects);
        
        if(destroyedObjects >= objectsToDestroy)
        {
            EnableDamage();
        }
    }
    
    private void EnableDamage()
    {
        hc.canTakeDamage = true;
    }
}
