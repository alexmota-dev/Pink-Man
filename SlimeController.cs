using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed;
    public float distance;
    private float initialPosition;
    private bool walkingToTheRight = true;
    private int lifes = 1;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.x;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentRightLeft(speed, distance, initialPosition);
        // MovimentRightLeft();
    }

    private void MovimentRightLeft(float speed, float distance, float initialPosition)
    {
        if (walkingToTheRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if (Mathf.Abs(transform.position.x - initialPosition) >= distance)
        {
            walkingToTheRight = !walkingToTheRight;
        }
    }

    private IEnumerator SlimeHit()
    {
        anim.SetBool("hit", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("hit", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.PlayerSuffersDamage(collision.gameObject);
        }
        if (collision.gameObject.tag == "Box")
        {
            StartCoroutine(SlimeHit());
            lifes--;
            if (lifes == 0)
            {
                StartCoroutine(SlimeHit());
                Destroy(gameObject, 1f);
            }
            else
            {
                StartCoroutine(SlimeHit());
            }
        }
    }
}
