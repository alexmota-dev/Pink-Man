using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    public float Hit;

    public bool isJumping;
    public bool doubleJump;
    public bool isOnFan;

    private Rigidbody2D rig;
    public Animator anim;
    private float speedY;
    public static PlayerController instance;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
        //revisar event Action<T>
        //revisar essa parte do FindObjectsOfType
        EnemyHeadController[] beeControllers = FindObjectsOfType<EnemyHeadController>();
        foreach (EnemyHeadController beeController in beeControllers)
        {
            //revisar essa parte da chamada função
            beeController.OnCollisionWithPlayer += DestroyBee;
        }  
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
        // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        // transform.position += movement * Time.deltaTime * speed;
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);
        walkAnimationUpdate(movement);
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isOnFan)
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
        if(collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Bullet")
        {
            GameController.instance.PlayerDies(gameObject);
        }


        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
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

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isOnFan = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isOnFan = false;
        }
    }

    private void DestroyBee(EnemyHeadController bee)
    {
        Animator beeAnimator = bee.transform.parent.GetComponent<Animator>();
        if (beeAnimator != null)
        {
            beeAnimator.SetBool("destroy", true);
        }
        GameController.instance.ImpulseUp(11f, gameObject.GetComponent<Rigidbody2D>());
        Destroy(bee.gameObject.transform.parent.gameObject, 0.5f);
        // bee.gameObject.transform.parent.gameObject.anim.SetBool("destroy", true);
    }

}
