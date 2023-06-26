using UnityEngine;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objeto = objetos[i];
    }

    private void Moviment()
    {
        // Calcula a direção do objetoB em relação ao objetoA
        if(objeto){
            Vector2 direcao = (objeto.position - transform.position).normalized;
            rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);

            // Calcula a direção do movimento
            Vector2 movementDirection = rb.position.normalized;

            if(objeto.position.x > gameObject.transform.position.x){
                transform.eulerAngles = new Vector3(0f,180f,0f);
            }
            if(objeto.position.x < gameObject.transform.position.x){
                transform.eulerAngles = new Vector3(0f,0,0f);
            }
        }
    }

    private void FixedUpdate()
    {
        Moviment();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Point"){
            if(ida){
                if(i+1 < objetos.Count){
                    objeto = objetos[++i];
                }
                else{
                    ida = false;
                }
            }
            if(!ida){
                if(i-1 >= 0){
                    objeto = objetos[--i];
                }
                else{
                    i = 0;
                    objeto = objetos[objetos.Count - 1];
                    ida = true;
                }
            }
        }
    }
}
