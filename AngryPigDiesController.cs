using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigDiesController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            GameController.instance.ImpulseUp(6f, collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(transform.parent.gameObject);
        }
    }
}
