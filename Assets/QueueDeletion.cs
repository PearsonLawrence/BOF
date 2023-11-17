using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueDeletion : MonoBehaviour
{
    public float Lifetime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Lifetime -= Time.deltaTime;
        if(Lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
