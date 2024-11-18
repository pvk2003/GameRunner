using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Events : MonoBehaviour
{
    public GameObject gamePausedPanel;  // Panel hiển thị khi game đang tạm dừng
    public Button pauseButton;  // Nút tạm dừng game

    private void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        // Xử lý tình huống khi game kết thúc
        if (PlayerManager.gameOver)
        {
            pauseButton.gameObject.SetActive(false); // Ẩn nút Pause khi game kết thúc
            gamePausedPanel.SetActive(false); // Đảm bảo panel Pause không hiển thị khi game kết thúc
            return;
        }
        else
        {
            pauseButton.gameObject.SetActive(true); // Hiển thị nút Pause khi game chưa kết thúc
        }

        // Kiểm tra nhấn phím Escape để tạm dừng hoặc tiếp tục game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerManager.isGamePaused)
            {
                ResumeGame();  // Tiếp tục game
                gamePausedPanel.SetActive(false);  // Ẩn panel Pause
            }
            else
            {
                PauseGame();  // Tạm dừng game
                gamePausedPanel.SetActive(true);  // Hiển thị panel Pause
            }
        }
    }

    // Hàm replay lại game (tải lại cấp độ)
    public void ReplayGame()
    {
        // Khởi tạo lại trạng thái game
        PlayerManager.gameOver = false;  // Đặt gameOver về false để game bắt đầu lại
        PlayerManager.isGamePaused = false;  // Đặt trạng thái game là đang chơi
        Time.timeScale = 1;  // Đảm bảo game tiếp tục

        // Tải lại cấp độ để bắt đầu lại game
        SceneManager.LoadScene("Level");
    }

    // Hàm quay về menu chính
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Tạm dừng game
    public void PauseGame()
    {
        if (!PlayerManager.isGamePaused && !PlayerManager.gameOver)  // Kiểm tra nếu game chưa tạm dừng và chưa kết thúc
        {
            Time.timeScale = 0;  // Dừng thời gian game
            PlayerManager.isGamePaused = true;  // Đánh dấu trạng thái game là tạm dừng
        }
    }

    // Tiếp tục game
    public void ResumeGame()
    {
        if (PlayerManager.isGamePaused && !PlayerManager.gameOver)  // Chỉ tiếp tục khi game đang tạm dừng và chưa kết thúc
        {
            Time.timeScale = 1;  // Tiếp tục thời gian game
            PlayerManager.isGamePaused = false;  // Đánh dấu trạng thái game là tiếp tục
        }
    }

    // Thoát game
    public void QuitGame()
    {
        Application.Quit();
    }
}
