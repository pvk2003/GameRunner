using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
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

    [SerializeField] Animator player_Animator;
    [SerializeField] private GameObject GameOverPanel;
    // [SerializeField] private GameObject tap_to_start_canvas;
    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        isGameOver = false;
        current_pos = 0;
    }

    void Update()
    {
        if (!isGameStarted || !isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Game is started");
                isGameStarted = true;
                player_Animator.SetInteger("isRunning", 1);
                player_Animator.speed = 1.5f;
                // tap_to_start_canvas.SetActive(false);
            }
        }
        if (isGameStarted)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + running_speed * Time.deltaTime);

            if (current_pos == 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    current_pos = 1;
                }

                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    current_pos = 2;
                }
            }
            else if (current_pos == 1)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    current_pos = 0;
                }
            }
            else if (current_pos == 2)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity = Vector3.up * jump_force;
                StartCoroutine(Jump());
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
        player_Animator.SetInteger("isSlide", 1);
        yield return new WaitForSeconds(1f); // Thời gian trượt
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
}
