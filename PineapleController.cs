using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineapleController : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private CircleCollider2D circleCollider;
    public GameObject Instruction;
    public bool HaveInstruction;
    public GameObject collected;
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
            PlayerController.instance.setShotSkillIsActive(true);
            if(HaveInstruction){
                Instruction.SetActive(true);
            }
            //desativando o spriteRenderer
            spriteRender.enabled = false;
            //desativando o circleCollider
            circleCollider.enabled = false;
            //ativando o gameObject collected
            collected.SetActive(true);

            //player ganha duas balas pra atirar
            //logica pra ele ganhar balas

            Destroy(gameObject,0.25f);
        }
    }
}
