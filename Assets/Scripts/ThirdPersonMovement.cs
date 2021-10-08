using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float playerSpeed;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    public bool isOnSlope = false;
    private Vector3 hitNormal;

    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {

        movePlayer = movePlayer * speed;
        controller.Move(movePlayer * Time.deltaTime);
        Debug.Log(controller.velocity.magnitude);

        SetGravity();

        PlayerSkills();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //we use normalize so we make sure we don't multiply the speed while pressing 2 buttons at the same time


        //magnitude equals to the length of the vector
        if (direction.magnitude >= 0.1f)
        {
            //you have to figure out how much you've got to rotate the Plyr on the Yaxis
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public void PlayerSkills()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    void SetGravity()
    {
        //gravity is negative so it is extracted from the player, making it fall to the ground

        if (controller.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        //this creates falling acceleration
    }

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        //calcula la dirección entre la posición actual y hacia arriba
    }

    void onControllerColliderHit(ControllerCollider hit)
    {
        hitNormal = hit.normal;
        //hitNormal references the default height of where our player is placed
    }
}
