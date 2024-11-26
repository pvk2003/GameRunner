using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;  // UI Text để hiển thị High Score
    public TextMeshProUGUI coinText;      // UI Text để hiển thị số coin

    void Start()
    {
        Time.timeScale = 1; // Đảm bảo game không bị pause
        UpdateUI();         // Cập nhật giao diện khi khởi chạy menu
    }

    void UpdateUI()
    {
        // Hiển thị High Score (quãng đường dài nhất)
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore + " m";

        // Hiển thị tổng số coin
        coinText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
    }

    public void PlayGames()
    {
        SceneManager.LoadScene("Level"); // Chuyển sang màn chơi chính
    }

    public void QuitGame()
    {
        Application.Quit(); // Thoát ứng dụng
        Debug.Log("Game Quit!");
    }
}
