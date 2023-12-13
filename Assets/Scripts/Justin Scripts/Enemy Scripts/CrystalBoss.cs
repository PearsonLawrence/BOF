using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBoss : MonoBehaviour
{
    public int objectsToDestroy = 4;
    private int destroyedObjects = 0;

    public GameObject hands;
  
    // Start is called before the first frame update
    void Start()
    {

        if (objectsToDestroy != 0)
        {
            hands.GetComponent<collisionDamageComponent>().enabled = true;
        }
        else
        {
            
            hands.GetComponent<collisionDamageComponent>().enabled = false;
            InitializeObjects();
        }
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
            hands.GetComponent<collisionDamageComponent>().enabled = true;
        }
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
