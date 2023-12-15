using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCheckComponent : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag == "Enemy")
        {
           
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
