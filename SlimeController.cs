using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed;
    public float distance;
    private float initialPosition;
    private bool walkingToTheRight = true;
    public int lifes = 6;
    private Animator anim;
    private bool slimeThisImmortal = false;
    public List<GameObject> hearts;
    public GameObject Trophy; 
    private int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.x;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MovimentRightLeft(speed, distance, initialPosition);
    }

    private void MovimentRightLeft(float speed, float distance, float initialPosition)
    {
        if (walkingToTheRight) {
            transform.eulerAngles = new Vector3(0f,180f,0f);
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else {
            transform.eulerAngles = new Vector3(0f,0f,0f);
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if (Mathf.Abs(transform.position.x - initialPosition) >= distance) {
            walkingToTheRight = !walkingToTheRight;
        }
    }

    public void SlimeSuffersDamage(){
        if(!slimeThisImmortal)
        {
            lifes--;
            Debug.Log("Tem " + lifes + "vidas");
            if(lifes == 0)
            {
                Debug.Log("Zero Vidas");
                StartCoroutine(SlimeDies());
            }
            else{
                GameObject heart = hearts[hearts.Count-i];
                i++;
                heart.SetActive(false);
                StartCoroutine(SlimeBecomesImmortal());
            }
        }
    }

    private IEnumerator SlimeBecomesImmortal()
    {
        slimeThisImmortal = true;
        anim.SetBool("hit",true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("hit",false);
        slimeThisImmortal = false;
    }

    private IEnumerator SlimeDies()
    {
        anim.SetBool("hit",true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("hit",false);
        Trophy.SetActive(true);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameController.instance.PlayerSuffersDamage(collision.gameObject);
        }
        if(collision.gameObject.tag == "Box")
        {
            SlimeSuffersDamage();
        }
    }
}
