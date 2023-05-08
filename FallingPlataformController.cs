using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlataformController : MonoBehaviour
{
    public float fallTime;
    private TargetJoint2D target;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("Falling", fallTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    void Falling()
    {
        target.enabled = false;
        boxCollider.isTrigger = true;
    }
}
