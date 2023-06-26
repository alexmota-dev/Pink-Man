using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator destroyBox()
    {
        anim.SetBool("breaking", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            StartCoroutine(destroyBox());
        }
    }
}
