using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBoss : MonoBehaviour
{
    public int objectsToDestroy = 4;
    private int destroyedObjects = 0;
    public List<GameObject> ObjectList;
    public HealthComponent hc;
  
    // Start is called before the first frame update
    void Start()
    {
        hc.canTakeDamage = false;
    }


    public void ObjectDestroyed()
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
