using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigController : MonoBehaviour
{
    public float speed;
    public float distance;

    private float initialPosition;
    private bool walkingToTheRight = true;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentRightLeft(speed, distance, initialPosition);
        // MovimentRightLeft();
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
        // Debug.Log(transform.position.x);
        if (Mathf.Abs(transform.position.x - initialPosition) >= distance) {
            walkingToTheRight = !walkingToTheRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Verifica a posição relativa do AngryPig em relação ao player
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 playerPosition = collision.gameObject.transform.position;

            if (contactPoint.y < playerPosition.y)
            {
                // Player pulou no topo do AngryPig, então o AngryPig é destruído
                Destroy(gameObject);
            }
            else
            {
                // Player encostou nas laterais do AngryPig, então o player é destruído
                GameController.instance.PlayerDies(collision.gameObject);
            }
        }
    }
}
