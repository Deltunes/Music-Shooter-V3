using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public AudioClip walkingSteps;
    public AudioClip runningSteps;
    public AudioSource footstepSound;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public int maxJumps = 1;
    private int jumpCount;
    public float jumpPower = 8f;
    public float gravity = 25f;

    public float lookSpeed = 2f;
    public float lookXLimit = 90f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    bool isRunning = false;

    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        jumpCount = maxJumps;
    }

    
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        forward.Normalize();
        right.Normalize();

        //Left shift to runz
        if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded)
        {
            isRunning = true;
        } 
        else
        {
            isRunning = false;
        }
        //bool isRunning = Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded;
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //Air Dash


        //Jump
        if (characterController.isGrounded && jumpCount < maxJumps)
        {
            jumpCount = maxJumps;
        }

        if (Input.GetKeyDown("space") && canMove && jumpCount > 0)
        {
            moveDirection.y = jumpPower;
            jumpCount--;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Footstep Sound
        if (characterController.isGrounded && (moveDirection.x != 0 || moveDirection.z != 0))
        {
            footstepSound.pitch = Random.Range(0.7f, 1.1f);
            if (!footstepSound.isPlaying)
            {
                if (!isRunning)
                {
                    footstepSound.PlayOneShot(walkingSteps);
                }
                else
                {
                    footstepSound.PlayOneShot(runningSteps);
                }
            }
        }
        else
        {
            footstepSound.Stop();
        }

        //Move Character
        characterController.Move(moveDirection * Time.deltaTime);

        //Rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
