using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{
    // Hàm reset toàn bộ dữ liệu PlayerPrefs và tải lại scene
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();  // Xóa toàn bộ dữ liệu trong PlayerPrefs
        PlayerPrefs.Save();       // Lưu thay đổi

        // Sau khi reset, có thể tải lại scene để game bắt đầu lại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
