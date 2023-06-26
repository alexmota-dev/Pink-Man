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
    public float speed;
    public float distance;
    private bool walkingToTheRight = true;
    private float initialPosition;

    void Start()
    {
        initialPosition = transform.position.x;
        anim = GetComponent<Animator>();
        StartCoroutine(GenerateBullets());
    }

    void Update()
    {
        Moviment(speed, distance, initialPosition);
    }
    private void Moviment(float speed, float distance, float initialPosition)
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

    IEnumerator GenerateBullets()
    {
        while (true)
        {
            anim.SetBool("attack", true);
            yield return new WaitForSeconds(0.25f);
            CreateBullet();
            yield return new WaitForSeconds(0.15f);
            anim.SetBool("attack", false);
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
