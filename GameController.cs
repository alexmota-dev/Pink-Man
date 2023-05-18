using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    //importe UnityEngine.UI pra usar o scoreText
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject NextLevel;

    public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PlayerDies(GameObject gameObject){
        Invoke("ShowGameOver",0.3f);
        PlayerController.instance.anim.SetTrigger("destroy");
        Destroy(gameObject, 0.3f);
    }

    public void ImpulseUp(float jumpForce, Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
}
