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
    public float VertOffset = 3;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.GetComponent<GameObject>();
        cam = this.GetComponent<Camera>();   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        toLocation = new Vector3(Focus.transform.position.x, Focus.transform.position.y + VertOffset, Focus.transform.position.z - 20);
        transform.position = Vector3.Lerp(transform.position, toLocation, LagSpeed * Time.unscaledDeltaTime) ;
    }
}
