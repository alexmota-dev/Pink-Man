using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private CircleCollider2D circleCollider;

    public GameObject collected;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            //desativando o spriteRenderer
            spriteRender.enabled = false;
            //desativando o circleCollider
            circleCollider.enabled = false;
            //ativando o gameObject collected
            collected.SetActive(true);

            GameController.instance.totalScore += score;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject,0.25f);
        }
    }
}
