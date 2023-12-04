using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{

    public float speed = 1f;
    public float rotation = 1f;
    public Transform wall;
    public Transform back;
    public Transform front;
    public Transform middle;
    public GameObject Player;

    private Collider2D frontBump;
    private Collider2D middleBump;
    private Collider2D backBump;
    private Collider2D wallBump;

    void Update()
    {
        frontBump = Physics2D.OverlapPoint(front.position, LayerMask.GetMask("ground"));
        backBump = Physics2D.OverlapPoint(back.position, LayerMask.GetMask("ground"));
        middleBump = Physics2D.OverlapPoint(middle.position, LayerMask.GetMask("ground"));
        wallBump = Physics2D.OverlapPoint(wall.position, LayerMask.GetMask("ground"));

        if(frontBump == null && middleBump == null && backBump == null && wallBump == null)
        {
            transform.Translate(new Vector3(speed, -1.5f * speed, 0f) * Time.deltaTime);
        }
        else
        {
            if(wallBump != null)
            {
                transform.Rotate(0f, 0f, .8f * rotation, Space.Self);
            }
            else if(middleBump != null && frontBump == null)
            {
                transform.Rotate(0f, 0f, -1.5f * rotation, Space.Self);
            }
            else if(frontBump != null && middleBump == null && backBump == null)
            {
                transform.Rotate(0, 0, 1.5f * rotation, Space.Self);
            }

            if (middleBump == null && wallBump == null)
            {
                transform.Translate(new Vector3(speed, 0f, 0f) * Time.deltaTime);
            }
            else if (middleBump == null && wallBump != null)
            {
                transform.Translate(new Vector3(-.2f * speed, -.2f * speed, 0f) * Time.deltaTime);
            }
            else if(middleBump != null && wallBump == null)
            {
                transform.Translate(new Vector3(1.25f * speed, speed, 0f) * Time.deltaTime);
            }
        }
    }


}

