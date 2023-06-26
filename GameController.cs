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
    public List<GameObject> lifes;
    private bool playerThisImmortal = false;
    private int i = 1;

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

    private IEnumerator PlayerBecomesImmortal()
    {
        playerThisImmortal = true;
        PlayerController.instance.anim.SetBool("destroy", true);
        yield return new WaitForSeconds(3f);
        PlayerController.instance.anim.SetBool("destroy", false);
        playerThisImmortal = false;
    }

    public void PlayerSuffersDamage(GameObject player)
    {
        if (!playerThisImmortal)
        {
            GameObject life = lifes[lifes.Count - i];
            i++;
            life.SetActive(false);
            StartCoroutine(PlayerBecomesImmortal());
            PlayerController.instance.lifes = PlayerController.instance.lifes - 1;
            Debug.Log("O player tem " + PlayerController.instance.lifes + " vidas");
            if (PlayerController.instance.lifes == 0)
            {
                PlayerDies(player);
            }
        }
    }

    private void PlayerDies(GameObject player)
    {
        Invoke("ShowGameOver", 0.3f);
        PlayerController.instance.anim.SetBool("destroy", true);
        Destroy(player, 0.3f);
    }

    public void ImpulseUp(float jumpForce, Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
}
