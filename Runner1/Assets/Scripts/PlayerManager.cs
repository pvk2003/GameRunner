
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    public GameObject newRecordPanel;

    public static int numberCoin;
    public Text coinsText;  // Hiển thị số lượng Coins
    public static int score;


    public static bool isGamePaused;

    // Thêm biến để theo dõi khoảng cách

    private Vector3 startPosition;

    void Start()
    {

        numberCoin = 0;
        Time.timeScale = 1;
        gameOver = isGameStarted = isGamePaused = false;
        startPosition = transform.position;  // Lưu vị trí ban đầu của nhân vật
    }

    void Update()
    {
        // Hiển thị số lượng Coins
        coinsText.text = "Coins: " + numberCoin;
        //coinsText.text = "Score: " + score;
        // Kiểm tra khi game over
        if (gameOver)
        {
            Time.timeScale = 0;  // Dừng thời gian khi game over
            gameOverPanel.SetActive(true);  // Hiển thị màn hình game over
        }

        // Bắt đầu game khi người chơi chạm
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText);  // Xóa thông báo bắt đầu game
        }
    }
}
