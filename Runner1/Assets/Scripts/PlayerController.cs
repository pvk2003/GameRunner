using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;
    public float LaneDistance = 4;
    public float jumpForce;
    public float Gravity = -20;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(!PlayerManager.isGameStarted){
        //     return;
        // }
        //Tang toc      
        if(forwardSpeed < maxSpeed){
            forwardSpeed += 0.1f*Time.deltaTime;
        }
        direction.z = forwardSpeed;

        if (controller.isGrounded)
        {
            // direction.y = 0;
            if (Input.GetKeyDown(KeyCode.UpArrow))//SwipeManager.swipeUp
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }
        //Xử lí chuyển lane 
        if(Input.GetKeyDown(KeyCode.RightArrow))// (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))// (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        //Vị trí mục tiêu để chuyển lane
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LaneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LaneDistance;
        }
        // transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
        // controller.center = controller.center;
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
