using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private Animator anim;
    public GameObject childComponent;
    public bool isChild = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ActivatingFire());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ActivatingFire()
    {
        while (true)
        {
            anim.SetBool("on", true);
            childComponent.SetActive(true);
            yield return new WaitForSeconds(5f);
            childComponent.SetActive(false);
            anim.SetBool("on", false);
            yield return new WaitForSeconds(3.5f);
            anim.SetBool("activating", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("activating", false);
            yield return new WaitForSeconds(1f);
        }
    }
}
