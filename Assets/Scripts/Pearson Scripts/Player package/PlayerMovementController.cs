using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float speed;
    public float Horz;
    public bool isCrouching;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Horz = Input.GetAxis("Horizontal");
        isCrouching = (Input.GetKey(KeyCode.S));
        if (isCrouching && transform.localScale != new Vector3(.5f,.25f, 1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .25f, transform.position.z);
            transform.localScale = new Vector3(.5f, .25f, 1);
        }
        else if(!isCrouching && transform.localScale == new Vector3(.5f, .25f, 1))
        {
            transform.localScale = new Vector3(.5f, .5f, 1);
        }

        rb.velocity = new Vector2(Horz * speed, rb.velocity.y);
        transform.forward = new Vector2(transform.forward.x, 0);

    }
}
