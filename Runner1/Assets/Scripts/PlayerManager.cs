using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    public GameObject newRecordPanel;

    public static int score; // Quãng đường đi được
    public Text scoreText; // Hiển thị quãng đường
    public TextMeshProUGUI CoinText; // Hiển thị số coin
    public TextMeshProUGUI newRecordText; // Hiển thị điểm cao mới
    public TextMeshProUGUI highScoreText; // Hiển thị High Score

    public static bool isGamePaused;

    private Vector3 startPosition; // Vị trí bắt đầu của nhân vật

    void Start()
    {
        score = 0; // Khởi tạo điểm số bằng 0
        Time.timeScale = 1; // Đảm bảo game chạy bình thường
        gameOver = isGameStarted = isGamePaused = false;
        startPosition = transform.position; // Lưu lại vị trí bắt đầu

        // Hiển thị High Score khi bắt đầu game
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore; // Cập nhật giao diện High Score
    }

    void Update()
    {
        if (isGameStarted && !gameOver)
        {
            // Tính quãng đường đã đi được
            score = Mathf.FloorToInt(Vector3.Distance(startPosition, transform.position));
        }

        // Cập nhật giao diện người dùng
        CoinText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString(); // Hiển thị số coin
        scoreText.text = score.ToString(); // Hiển thị quãng đường

        // Khi game over
        if (gameOver)
        {
            Time.timeScale = 0;

            // Kiểm tra nếu đạt điểm cao mới
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                newRecordPanel.SetActive(true);
                newRecordText.text = "New \nRecord\n" + score;
                PlayerPrefs.SetInt("HighScore", score); // Lưu điểm cao mới
            }

            // Hiển thị điểm cao nhất
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);

            gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }

        // Bắt đầu game khi nhấn tap
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText); // Xóa text bắt đầu
        }
    }
}
