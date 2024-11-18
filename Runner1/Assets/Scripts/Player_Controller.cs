using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] CapsuleCollider controller;
    [SerializeField] Transform center_pos;
    [SerializeField] Transform right_pos;
    [SerializeField] Transform left_pos;
    [SerializeField] Rigidbody rb;

    int current_pos = 0;

    public float side_speed;
    public float running_speed;
    public float jump_force;

    // Thêm các biến tăng tốc độ
    public float speedIncrease = 0.1f;  // Tốc độ gia tăng mỗi giây
    public float maxRunningSpeed = 50f; // Giới hạn tốc độ tối đa

    bool isGameStarted = false;
    bool isGameOver = false;
    bool isSlide = false;
    bool isGrounded = true;

    [SerializeField] Animator player_Animator;
    [SerializeField] private GameObject GameOverPanel;

    // Biến để theo dõi khoảng cách
    private float distanceTraveled = 0f;

    void Start()
    {
        isGameStarted = false;
        isGameOver = false;
        current_pos = 0;
        running_speed = 10f; // Khởi tạo tốc độ chạy bằng 10
    }

    void Update()
    {
        // Kiểm tra nếu nhân vật đang chạm đất
        isGrounded = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);
        
        if (!isGameStarted || isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Game is started");
                isGameStarted = true;
                player_Animator.SetInteger("isRunning", 1);
                player_Animator.speed = 1.5f;
            }
        }

        if (isGameStarted)
        {
            // Tăng tốc độ chạy theo thời gian
            running_speed += speedIncrease * Time.deltaTime;
            running_speed = Mathf.Clamp(running_speed, 0, maxRunningSpeed); // Giới hạn tốc độ chạy

            // Tính toán khoảng cách đã di chuyển
            distanceTraveled += running_speed * Time.deltaTime;

            // Di chuyển nhân vật về phía trước với tốc độ hiện tại
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + running_speed * Time.deltaTime);

            // Di chuyển sang trái hoặc phải
            HandleSideMovement();

            // Nhảy khi nhấn mũi tên lên
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                rb.velocity = Vector3.up * jump_force;
                StartCoroutine(Jump());
                isGrounded = false;
            }

            // Trượt khi nhấn mũi tên xuống
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isSlide)
            {
                StartCoroutine(Slide());
            }
        }

        if (isGameOver && !GameOverPanel.activeSelf)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void HandleSideMovement()
    {
        if (current_pos == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) current_pos = 1;
            else if (Input.GetKeyDown(KeyCode.RightArrow)) current_pos = 2;
        }
        else if (current_pos == 1 && Input.GetKeyDown(KeyCode.RightArrow)) current_pos = 0;
        else if (current_pos == 2 && Input.GetKeyDown(KeyCode.LeftArrow)) current_pos = 0;

        Vector3 targetPos = center_pos.position;
        if (current_pos == 1) targetPos = left_pos.position;
        else if (current_pos == 2) targetPos = right_pos.position;

        if (Vector3.Distance(transform.position, new Vector3(targetPos.x, transform.position.y, transform.position.z)) >= 0.1f)
        {
            Vector3 dir = targetPos - transform.position;
            transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
        }
    }

    IEnumerator Jump()
    {
        player_Animator.SetInteger("isJump", 1);
        yield return new WaitForSeconds(0.5f);
        player_Animator.SetInteger("isJump", 0);
    }

    IEnumerator Slide()
    {
        isSlide = true;

        // Thay đổi thuộc tính CapsuleCollider
        Vector3 originalCenter = controller.center;
        float originalHeight = controller.height;

        controller.center = new Vector3(originalCenter.x, -0.5f, originalCenter.z); // Đặt center y là -0.5
        controller.height = 1f; // Đặt height là 1

        player_Animator.SetInteger("isSlide", 1);
        yield return new WaitForSeconds(1f); // Thời gian trượt

        // Khôi phục lại thuộc tính CapsuleCollider
        controller.center = originalCenter;
        controller.height = originalHeight;

        player_Animator.SetInteger("isSlide", 0);
        isSlide = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            isGameStarted = false;
            isGameOver = true;
            player_Animator.applyRootMotion = true;
            player_Animator.SetInteger("isDied", 1);
        }
    }

    // Hàm để lấy khoảng cách đã di chuyển
    public float GetDistanceTraveled()
    {
        return distanceTraveled;
    }
}
