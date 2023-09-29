using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject obj;
    private Camera cam;
    public GameObject Focus;
    public float LagSpeed;
    private Vector3 toLocation;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.GetComponent<GameObject>();
        cam = this.GetComponent<Camera>();   
    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        toLocation = new Vector3(Focus.transform.position.x, Focus.transform.position.y, Focus.transform.position.z - 20);
        transform.position = Vector3.Lerp(transform.position, toLocation, LagSpeed) ;
    }
}
