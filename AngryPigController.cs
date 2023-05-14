using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigController : MonoBehaviour
{
    public float velocidade;
    public float distancia;
    private float posicaoInicial;
    private bool indoParaDireita = true;
    // Start is called before the first frame update
    void Start()
    {
        posicaoInicial = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (indoParaDireita) {
            transform.eulerAngles = new Vector3(0f,180f,0f);
            transform.position += Vector3.right * velocidade * Time.deltaTime;
        } else {
            transform.eulerAngles = new Vector3(0f,0f,0f);
            transform.position -= Vector3.right * velocidade * Time.deltaTime;
        }

        if (Mathf.Abs(transform.position.x - posicaoInicial) >= distancia) {
            indoParaDireita = !indoParaDireita;
        }
    }
}
