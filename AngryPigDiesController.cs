using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigDiesController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AngryPigController.instance.anim.SetTrigger("destroy");
            GameController.instance.ImpulseUp(10f, collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(transform.parent.gameObject, 0.3f);
        }
    }
}
