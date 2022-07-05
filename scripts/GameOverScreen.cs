using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOverScreen : MonoBehaviour
{

    public Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = " Score : " + score.ToString();
    }
    public void restartButton()
    {
       //SoundManagerScript.PlaySound("gameover");
        SceneManager.LoadScene("snakegame");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
