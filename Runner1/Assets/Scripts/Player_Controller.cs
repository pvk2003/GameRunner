using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
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

    bool isGameStarted = false;
    bool isGameOver = false;
    bool isSlide = false;
    bool isGrounded = true;

    [SerializeField] Animator player_Animator;
    [SerializeField] private GameObject GameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        isGameOver = false;
        current_pos = 0;
    }

    void Update()
    {
        // Kiểm tra nếu nhân vật đang chạm đất
        isGrounded = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);
        if (!isGameStarted || !isGameOver)
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
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + running_speed * Time.deltaTime);

            if (current_pos == 0)
            {
                if (SwipeManager.swipeLeft)
                {
                    current_pos = 1;
                }

                else if (SwipeManager.swipeRight)
                {
                    current_pos = 2;
                }
            }
            else if (current_pos == 1)
            {
                if (SwipeManager.swipeRight)
                {
                    current_pos = 0;
                }
            }
            else if (current_pos == 2)
            {
                if (SwipeManager.swipeLeft)
                {
                    current_pos = 0;
                }
            }

            if (current_pos == 0)

            {
                if (Vector3.Distance(transform.position, new Vector3(center_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(center_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }
            else if (current_pos == 1)
            {
                if (Vector3.Distance(transform.position, new Vector3(left_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(left_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }
            else if (current_pos == 2)
            {
                if (Vector3.Distance(transform.position, new Vector3(right_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(right_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }

            if (SwipeManager.swipeUp && isGrounded)
            {
                rb.velocity = Vector3.up * jump_force;
                StartCoroutine(Jump());
                isGrounded = false;
            }

            // Trượt khi nhấn mũi tên xuống
            if (SwipeManager.swipeDown && !isSlide)
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
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }
}