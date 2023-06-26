using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBodyController : MonoBehaviour
{
    private int livesPig = 2;
    private bool immunity = false;
    public string enemyName;
    //alterei o nome pra ficar o o body já que o tipo dela é diferente, o nome precisa ser corrigido posteriormente.
    public event Action<EnemyBodyController> OnCollisionWithBullet;

    public GameObject pineapplePrefab;

    IEnumerator WaitImmunity()
    {
        immunity = true;
        yield return new WaitForSeconds(2f);
        immunity = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.PlayerSuffersDamage(collision.gameObject);
        }
        if (collision.gameObject.tag == "BulletPlayer")
        {
            switch (enemyName)
            {
                case "Pig":
                    if (livesPig == 1)
                    {
                        if (!immunity)
                        {
                            AngryPigController.instance.anim.SetBool("destroy", true);
                            GameController.instance.ImpulseUp(11f, collision.gameObject.GetComponent<Rigidbody2D>());
                            Destroy(transform.parent.gameObject, 0.45f);
                        }
                    }
                    if (livesPig > 1)
                    {
                        AngryPigController.instance.anim.SetBool("hitOne", true);
                        GameController.instance.ImpulseUp(11f, collision.gameObject.GetComponent<Rigidbody2D>());
                        // AngryPigController.instance.ExpectToGetAngry();
                        AngryPigController.instance.DoubleSpeed();
                        AngryPigController.instance.anim.SetBool("isAngry", true);
                        StartCoroutine(WaitImmunity());
                        livesPig--;
                    }
                    break;
                case "Bee":
                    OnCollisionWithBullet?.Invoke(this);
                    break;
                case "Plant":
                    pineapplePrefab.SetActive(true);
                    OnCollisionWithBullet?.Invoke(this);
                    break;
                default:
                    break;
            }
        }
    }
}
