using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float intervaloDeGeracao = 3f;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(GerarBalas());
        // StartCoroutine(AnimatorAttackStart());
        // StartCoroutine(AnimatorAttackEnd());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // IEnumerator AnimatorAttackStart()
    // {
    //     while (true)
    //     {
    //         anim.SetBool("attack",true);
    //         yield return new WaitForSeconds(startAnimationInterval);
    //     }
    // }
    // IEnumerator AnimatorAttackEnd()
    // {
    //     while (true)
    //     {
    //         anim.SetBool("attack",false);
    //         yield return new WaitForSeconds(intervaloDeGeracao);
    //     }
        
    // }
    
    IEnumerator  GerarBalas()
    {
        while (true)
        {
            anim.SetBool("attack", true);
            yield return new WaitForSeconds(0.25f);
            CreateBullet();
            yield return new WaitForSeconds(0.15f);
            anim.SetBool("attack", false);
            // CreateBullet();
            yield return new WaitForSeconds(intervaloDeGeracao);
        }
    }

    void CreateBullet()
    {
        // Instancia a bala
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        // Adiciona velocidade à bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
    }
}
