using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI CoinText;

    // public Animator messageAnim;

    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        highScoreText.text = "High Score\n" + PlayerPrefs.GetInt("HighScore", 0);
        CoinText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
    }
    // Start is called before the first frame update
    public void PlayGames()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
