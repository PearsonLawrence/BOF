using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public CrystalBoss boss;
        
    public void doDestroy()
    {
        boss.ObjectDestroyed();
    }
}
