using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    public float Hit;

    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    public Animator anim;
    private float speedY;
    public static PlayerController instance;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        fallAndUpAnimationUpdate();
        Move();
        Jump();
    }

    private void fallAndUpAnimationUpdate()
    {
        speedY = gameObject.GetComponent<Rigidbody2D>().velocity.y;
        fallAndUpAnimationTransition(speedY);
    }

    private void fallAndUpAnimationTransition(float speedY)
    {
        if (speedY > 0)
        {
            anim.SetBool("up",true);
        }
        else if (speedY < 0)
        {
            anim.SetBool("fall",true);
        }
        else if(speedY == 0)
        {
            anim.SetBool("fall",false);
            anim.SetBool("up",false);
        }
    }
    
    private void walkAnimationUpdate(float inputGetAxis)
    {
        if(inputGetAxis > 0f)
        {
            //andando pra direita
            anim.SetBool("walk",true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        if(inputGetAxis < 0f)
        {
            //andando pra direita
            anim.SetBool("walk",true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        if(inputGetAxis == 0)
        {
            anim.SetBool("walk",false);
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        walkAnimationUpdate(Input.GetAxis("Horizontal"));
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                GameController.instance.ImpulseUp(JumpForce, rig);
                doubleJump = true;
            }
            else{
                if(doubleJump)
                {
                    GameController.instance.ImpulseUp(JumpForce, rig);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            GameController.instance.PlayerDies(gameObject);
        }


        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
 
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
           isJumping = true; 
        }
    }
}
