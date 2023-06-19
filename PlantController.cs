using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public Animator anim;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform shootingPoint;
    private float intervaloDeGeracao = 2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(GenerateBullets());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator  GenerateBullets()
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
        if(gameObject.transform.rotation.eulerAngles.y == 0f)
        {
            rb.velocity = Vector2.left * bulletSpeed;
        }
        if(gameObject.transform.rotation.eulerAngles.y == 180f)
        {
            rb.velocity = Vector2.right * bulletSpeed;
        }
    }
}
