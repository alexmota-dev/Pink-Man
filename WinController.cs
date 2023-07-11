using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelName;
    void Start()
    {
        StartCoroutine(ModifyScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ModifyScene()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Muda de cena");
        GameController.instance.RestartGame(levelName);
    }
}
