using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointA : MonoBehaviour
{
    public static bool hasBox = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Box"){
            hasBox = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Box"){
            hasBox = false;
        }
    }
}
