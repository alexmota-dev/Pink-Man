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

    private int numberOfShots = 0;
    private bool shotSkillIsActive = false;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform shootingPoint;

    public int lifes = 4;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
        //revisar event Action<T>
        //revisar essa parte do FindObjectsOfType
        EnemyHeadController[] enemyHeadControllers = FindObjectsOfType<EnemyHeadController>();
        foreach (EnemyHeadController enemyController in enemyHeadControllers)
        {
            //revisar essa parte da chamada função
            enemyController.OnCollisionWithPlayer += DestroyEnemy;
        }

        EnemyBodyController[] enemyBodyControllers = FindObjectsOfType<EnemyBodyController>();
        foreach (EnemyBodyController enemyController in enemyBodyControllers)
        {
            //revisar essa parte da chamada função
            enemyController.OnCollisionWithBullet += DestroyEnemy;
        } 
        // Debug.Log("New Scene");
        // GameController.instance.UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        ShotSkill();
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

    void ShotSkill()
    {
        if(Input.GetButtonDown("Fire1") && shotSkillIsActive && numberOfShots > 0)
        {
            CreateBullet();
            numberOfShots--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Bullet")
        {
            Debug.Log("A bala acertou o player !");
            GameController.instance.PlayerSuffersDamage(gameObject);
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

    private void DestroyEnemy(EnemyHeadController enemy)
    {
        Animator enemyAnimator = enemy.transform.parent.GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.SetBool("destroy", true);
        }
        GameController.instance.ImpulseUp(11f, gameObject.GetComponent<Rigidbody2D>());
        Destroy(enemy.gameObject.transform.parent.gameObject, 0.5f);
        // bee.gameObject.transform.parent.gameObject.anim.SetBool("destroy", true);
    }

    private void DestroyEnemy(EnemyBodyController enemy)
    {
        Animator enemyAnimator = enemy.transform.parent.GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.SetBool("destroy", true);
        }
        GameController.instance.ImpulseUp(11f, gameObject.GetComponent<Rigidbody2D>());
        Destroy(enemy.gameObject.transform.parent.gameObject, 0.5f);
        // bee.gameObject.transform.parent.gameObject.anim.SetBool("destroy", true);
    }

    public void setShotSkillIsActive(bool value)
    {
        shotSkillIsActive = value;
        numberOfShots +=2;
    }

    void CreateBullet()
    {
        // Instancia a bala
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        // Adiciona velocidade à bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(gameObject.transform.rotation.eulerAngles.y == 0f)
        {
            rb.velocity = Vector2.right * bulletSpeed;
        }
        if(gameObject.transform.rotation.eulerAngles.y == 180f)
        {
            rb.velocity = Vector2.left * bulletSpeed;
        }
    }
}
