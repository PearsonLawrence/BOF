using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length);

        if (!objects[rand]) return;

        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] objects;

}
