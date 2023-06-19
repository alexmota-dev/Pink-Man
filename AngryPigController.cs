using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigController : MonoBehaviour
{
    public float speed;
    public float distance;

    private float initialPosition;
    private bool walkingToTheRight = true;
    public Animator anim;
    public static AngryPigController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        initialPosition = transform.position.x;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentRightLeft(speed, distance, initialPosition);
        // MovimentRightLeft();
    }

    private void MovimentRightLeft(float speed, float distance, float initialPosition)
    {
        if (walkingToTheRight) {
            transform.eulerAngles = new Vector3(0f,180f,0f);
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else {
            transform.eulerAngles = new Vector3(0f,0f,0f);
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if (Mathf.Abs(transform.position.x - initialPosition) >= distance) {
            walkingToTheRight = !walkingToTheRight;
        }
    }

    public void DoubleSpeed()
    {
        StartCoroutine(Expect());
    }

    IEnumerator Expect()
    {
        float oldSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(0.3f);
        speed = oldSpeed * 2;
    }

    // public void ExpectToGetAngry(){
    //     StartCoroutine(Expect());
    // }

}
