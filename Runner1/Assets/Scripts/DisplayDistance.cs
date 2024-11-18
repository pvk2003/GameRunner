using UnityEngine;
using UnityEngine.UI;

public class DistanceDisplay : MonoBehaviour
{
    public Text distanceText;  // UI Text để hiển thị khoảng cách
    private Vector3 startPosition;  // Vị trí bắt đầu
    private float distanceTravelled = 0f;  // Khoảng cách đã di chuyển

    void Start()
    {
        // Lưu lại vị trí ban đầu khi game bắt đầu
        startPosition = transform.position;
    }

    void Update()
    {
        // Tính khoảng cách giữa vị trí hiện tại và vị trí bắt đầu
        distanceTravelled = Vector3.Distance(startPosition, transform.position);

        // Cập nhật giá trị khoảng cách lên UI
        distanceText.text = "Distance: " + Mathf.FloorToInt(distanceTravelled) + " m";
    }
}
