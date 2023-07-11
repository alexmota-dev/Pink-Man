using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    public float JumpForce;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }  
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("jump");
        }
    }
}
