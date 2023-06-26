using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string bulletName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (bulletName == "BulletEnemy")
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet" || collision.gameObject.layer == 7)
            {
                Destroy(gameObject);
            }
        }
        if (bulletName == "BulletPlayer")
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 7 || collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }
    }
}
