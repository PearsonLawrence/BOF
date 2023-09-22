using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement_Controller : MonoBehaviour
{

    public GameObject GO;
    public Rigidbody2D RB;
    public float speed = 100, accel = 10;
    public float Horz, Vert;
    // Start is called before the first frame update
    void Start()
    {
        GO = this.gameObject;
        RB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horz = Input.GetAxis("Horizontal");
        Vert = Input.GetAxis("Vertical");
        RB.velocity = new Vector2(Horz, RB.velocity.y);
        transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);

        if (Vert > 0 && RB.velocity.y == 0)
        {
            RB.AddForce(new Vector2(0, 100));
        }
    }
}
