using UnityEngine;
using System.Collections.Generic;

public class MovesBetweenPoints : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    private Transform point;
    public float velocidade = 2f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        point = startPoint;
    }

    private void Moviment()
    {
        // Calcula a dire��o do objetoB em rela��o ao objetoA
        if (point)
        {
            Vector2 direcao = (point.position - transform.position).normalized;
            // if(direcao[0] <= 0f)
            // {
            //     Debug.Log("chegou no destino " + direcao[0]);
            // }
            // Rotaciona o objetoA para olhar na dire��o do objeto no entanto no modelo 2D isso n�o � usual.
            // transform.LookAt(objetoB);
            rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
        }
    }

    private void FixedUpdate()
    {
        Moviment();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Point")
        {
            if (point == startPoint)
            {
                point = endPoint;
            }
            else
            {
                point = startPoint;
            }
        }
    }
}
