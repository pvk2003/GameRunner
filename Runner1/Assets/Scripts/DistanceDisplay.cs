using UnityEngine;
using UnityEngine.UI;

public class DistanceDisplay : MonoBehaviour
{
    public Text distanceText;  // UI Text để hiển thị khoảng cách
    private float startZ;      // Giá trị Z ban đầu
    private float distanceTravelled = 0f;  // Khoảng cách đã di chuyển

    void Start()
    {
        // Lưu giá trị Z ban đầu
        startZ = transform.position.z;
    }

    void Update()
    {
        // Tính khoảng cách dựa trên sự thay đổi tọa độ Z
        distanceTravelled = transform.position.z - startZ;

        // Đảm bảo giá trị không âm (phòng trường hợp nhân vật di chuyển lùi)
        distanceTravelled = Mathf.Max(0, distanceTravelled);

        // Cập nhật giá trị khoảng cách lên UI
        distanceText.text = "" + Mathf.FloorToInt(distanceTravelled) + " m";
        
        // Lưu quãng đường nếu là High Score mới
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (distanceTravelled > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", Mathf.FloorToInt(distanceTravelled)); // Cập nhật High Score
        }
    }
}
