using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadController : MonoBehaviour
{
    private int lives=2;
    private bool immunity = false;
    public string enemyName;

    IEnumerator WaitImmunity()
    {
        immunity = true;
        yield return new WaitForSeconds(2f);
        immunity = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (enemyName)
        {
            case "Pig":
                if(collision.gameObject.tag == "Player")
                {
                    if(lives == 1)
                    {
                        if(!immunity)
                        {
                            AngryPigController.instance.anim.SetBool("destroy",true);
                            GameController.instance.ImpulseUp(11f, collision.gameObject.GetComponent<Rigidbody2D>());
                            Destroy(transform.parent.gameObject, 0.45f);
                        }
                    }
                    if(lives > 1)
                    {
                        AngryPigController.instance.anim.SetBool("hitOne",true);
                        GameController.instance.ImpulseUp(11f, collision.gameObject.GetComponent<Rigidbody2D>());
                        // AngryPigController.instance.ExpectToGetAngry();
                        AngryPigController.instance.DoubleSpeed();
                        AngryPigController.instance.anim.SetBool("isAngry",true);
                        StartCoroutine(WaitImmunity());
                        lives--;
                    }
                }
                break;
            case "Bee":
                break;
            default:
                break;
        }
    }
}
