using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static int numberCoin;
    public Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        numberCoin = 0;
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = "Coins: " + numberCoin;
    }
}
