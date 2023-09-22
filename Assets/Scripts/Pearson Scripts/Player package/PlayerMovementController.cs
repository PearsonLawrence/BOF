using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float speed;
    public float Horz;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horz = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Horz * speed, rb.velocity.y);
        transform.forward = new Vector2(transform.forward.x, 0);
    }
}
