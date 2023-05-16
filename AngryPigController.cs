using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigController : MonoBehaviour
{
    public float speed;
    public float distance;

    private float initialPosition;
    private bool walkingToTheRight = true;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.x;
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
}
