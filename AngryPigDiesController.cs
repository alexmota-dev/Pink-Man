using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigDiesController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("AngryPig Morre");
        }
    }
}
