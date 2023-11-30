using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StarTeleport : MonoBehaviour
{

    public float teleportRange;
    public float teleportInterval;
    public float maxYBound = 4f;
    public float maxXBound = 10f;
    public float minYBound = -4f;
    public float minXBound = -10f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TeleportRoutine());
    }


    IEnumerator TeleportRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(teleportInterval);

            Teleport();
        }
    }

    void Teleport()
    {
        
        
        Vector3 randomPosition = GetRandomPosition();

        // Clamp the x and z coordinates within bounds
        //float clampedX = Mathf.Clamp(randomPosition.x, minBounds.x, maxBounds.x);
        //float clampedZ = Mathf.Clamp(randomPosition.z, minBounds.y, maxBounds.y);

        // Apply the clamped position
        transform.position = new Vector3(Random.Range(minXBound, maxXBound), Random.Range(minYBound, maxYBound), 0f);

        //transform.position = new Vector3(randomPosition.x, randomPosition.y, randomPosition.z);

        Debug.Log("Enemy teleported to: " + transform.position);
    }

    Vector3 GetRandomPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * teleportRange;
        Vector3 randomPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0f);

        return randomPosition;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
