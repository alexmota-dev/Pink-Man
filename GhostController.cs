using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour
{
    public Transform objeto;
    public List<Transform> objetos;
    public float velocidade = 5f;
    private Rigidbody2D rb;
    private int i = 0;
    private bool ida = true;
    private float speedY;
    private Vector2 lastPosition;
    private Animator anim;
    private BoxCollider2D box;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        objeto = objetos[i];
    }

    private void Moviment()
    {
        // Calcula a dire��o do objetoB em rela��o ao objetoA
        if (objeto)
        {
            Vector2 direcao = (objeto.position - transform.position).normalized;
            rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);

            // Calcula a dire��o do movimento
            Vector2 movementDirection = rb.position.normalized;

            if (objeto.position.x > gameObject.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (objeto.position.x < gameObject.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0f, 0, 0f);
            }
        }
    }

    private void FixedUpdate()
    {
        Moviment();
    }

    private IEnumerator waitAnim()
    {
        float speedLast = PlayerController.instance.speed;
        PlayerController.instance.speed = PlayerController.instance.speed / 2;
        anim.SetBool("playerTouch", true);
        box.enabled = false;
        yield return new WaitForSeconds(2f);
        anim.SetBool("playerTouch", false);
        //ghost desaparece aqui
        anim.SetBool("appear", true);
        yield return new WaitForSeconds(2f);
        box.enabled = true;
        anim.SetBool("appear", false);
        StartCoroutine(waitAnim());
        PlayerController.instance.speed = speedLast;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Point")
        {
            if (ida)
            {
                if (i + 1 < objetos.Count)
                {
                    objeto = objetos[++i];
                }
                else
                {
                    ida = false;
                }
            }
            if (!ida)
            {
                if (i - 1 >= 0)
                {
                    objeto = objetos[--i];
                }
                else
                {
                    i = 0;
                    objeto = objetos[objetos.Count - 1];
                    ida = true;
                }
            }
        }

        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(waitAnim());
        }
    }
}
