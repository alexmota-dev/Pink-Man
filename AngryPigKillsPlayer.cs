using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigKillsPlayer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameController.instance.PlayerDies(collision.gameObject);
        }
    }
}