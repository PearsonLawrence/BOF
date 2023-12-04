using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSmooth : MonoBehaviour
{

    public Transform followMe;
    public float moveSmoothing = 1f;
    public float rotationSmoothing = 1f;
    private Vector3 moveFrom;
    private Vector3 moveTo;
    private Vector3 velocity = Vector3.zero;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveFrom = new Vector3(transform.position.x, transform.position.y, transform.position.z);   
        moveTo = new Vector3(followMe.transform.position.x, followMe.transform.position.y, followMe.transform.position.z);

        transform.position = Vector3.SmoothDamp(moveFrom, moveTo, ref velocity, Time.deltaTime * moveSmoothing);
        transform.rotation = Quaternion.Lerp(transform.rotation, followMe.rotation, Time.deltaTime * rotationSmoothing);
    }

    
}
