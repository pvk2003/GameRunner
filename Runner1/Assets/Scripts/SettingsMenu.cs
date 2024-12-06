using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;  // Bảng cài đặt
    public Button settingsButton;     // Nút Settings
    public Button closeButton;        // Nút đóng Settings
    public Toggle muteToggle;         // Checkbox để tắt/bật âm thanh

    private bool isMuted = true; // Biến lưu trạng thái âm thanh
    
    void Start()
    {
        // Đảm bảo rằng bảng Settings bị ẩn khi mới vào game
        settingsPanel.SetActive(false);
        
        // Đặt trạng thái âm thanh ban đầu
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        muteToggle.isOn = isMuted;
        UpdateSound();

        // Đăng ký sự kiện cho các nút
        settingsButton.onClick.AddListener(OpenSettings);
        closeButton.onClick.AddListener(CloseSettings);
        muteToggle.onValueChanged.AddListener(ToggleMute);

        
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true); // Hiển thị bảng cài đặt
        Time.timeScale = 0; // Dừng game khi mở bảng cài đặt
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false); // Ẩn bảng cài đặt
        Time.timeScale = 1; // Tiếp tục game khi đóng bảng cài đặt
    }

    void ToggleMute(bool isOn)
    {
        isMuted = isOn;
        UpdateSound();
    }

    void UpdateSound()
    {
        if (isMuted)
        {
            AudioListener.volume = 1;  // Tắt âm thanh
            PlayerPrefs.SetInt("Muted", 0); // Lưu trạng thái tắt âm
        }
        else
        {
            AudioListener.volume = 0;  // Bật âm thanh
            PlayerPrefs.SetInt("Muted", 1); // Lưu trạng thái bật âm
        }
    }
}
