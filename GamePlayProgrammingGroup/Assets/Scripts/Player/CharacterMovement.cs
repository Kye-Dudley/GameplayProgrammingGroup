using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    //Camera
    public Transform cam;
    private Vector3 camF;
    private Vector3 camR;

    //Movement
    private CharacterController controller;
    Vector2 movementVector;
    private float movementSpeed;
    public float acceleration = 10;
    public float rotationSpeed = 10;
    public float jumpHeight = 5;
    private bool jumpInput;
    public float groundSpeed = 10;
    public float airControl = 7;
    public float JumpCount;
    public float maxJumpCount;
    private bool takejumpcountOnce = false;
    [HideInInspector]
    public bool interactInput;

    //Locking On
    public bool locked_on;
    public Text lockedtext;
    public Camera main_cam;
    private float sensitivityX = 10.0f;
    private Vector3 planardirection;
    private Transform target;
    public bool buffer;
    public float timer;

    //Physics
    Vector3 intendedDirection;
    Vector3 velocity;
    Vector3 velocityXZ;
    public float gravity = 9.81f;
    public bool MovingOnGround = false;
    bool resetGrav;

    private float cyoteTime = 0.15f;
    private float jumpBuffer = 0.1f;
    private float jumpBufferTimer;
    private float playerAirTime;
    private Vector3 normalHitAngle;

    //Animation
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        lockedtext.enabled = false;
    }

    private void OnMove(InputValue movementValue)
    {
        //Setting the movement vector to our input on X and Y.
        //movementVector is the equivalent of input.getAxis for the new input system.
        movementVector = movementValue.Get<Vector2>();
    }

    private void OnInteract()
    {
        interactInput = true;
    }

    private void OnJump()
    {
        jumpInput = true;
    }

    private void Update()
    {
        calculateInput();
        calculateCamera();
        calculateMovement();
        calculateGravity();
        updateAnimations();

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            buffer = false;
        }

        if (locked_on && target != null)
        {
            Vector3 camtotarget = target.position - main_cam.transform.position;
            Vector3 planarcam = Vector3.ProjectOnPlane(camtotarget, Vector3.up);
            planardirection = planarcam != Vector3.zero ? planarcam.normalized : planardirection;

            Quaternion targetrotation = Quaternion.LookRotation(planardirection);
            Quaternion newrotation = Quaternion.Slerp(main_cam.transform.rotation, targetrotation, Time.deltaTime * sensitivityX);
            main_cam.transform.rotation = newrotation;

            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

            if (Input.GetButton("Lockon") && buffer == false)
            {
                locked_on = true;
                target = null;
                buffer = true;
                lockedtext.enabled = false;
                timer = 1;

            }
        }

        if (MovingOnGround == true)
        {
            movementSpeed = groundSpeed;
        }
        else
        {
            movementSpeed = airControl;
        }


        if (jumpInput == true)
        {
            jumpBufferTimer = jumpBuffer;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (jumpBufferTimer > 0)
        {
            calculateJump();
        }


        //Resets the player's velocity when they hit a ceiling. 
        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            if (velocity.y > 0)
            {
                velocity.y = -1;
            }
        }

        //Sliding Down Slopes
        if (controller.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slope, 5f))
        {
            normalHitAngle = slope.normal;
            if (Vector3.Angle(normalHitAngle, Vector3.up) > controller.slopeLimit)
            {
                velocity += new Vector3(normalHitAngle.x /1.5f, -normalHitAngle.y, normalHitAngle.z /1.5f);
            }
        }

        //Update the player's movement after movePlayer() has been updated.
        controller.Move(velocity * Time.deltaTime);

        calculateGround();

        jumpInput = false;
        interactInput = false;
    }

    void updateAnimations()
    {
        animator.SetFloat("MovSpeed", controller.velocity.magnitude);
        animator.SetFloat("FallSpeed", velocity.y);
        animator.SetBool("OnGround", MovingOnGround);
    }

    void calculateInput()
    {

        //Clamp the vector so it can only reach a maximum of 1.
        movementVector = Vector2.ClampMagnitude(movementVector, 1);

    }

    void calculateCamera()
    {
        //camF is forwards camera direction. camR is right camera direction.
        camF = cam.forward;
        camR = cam.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void calculateGround()
    {

        if ((controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            playerAirTime = 0;
            MovingOnGround = true;
            JumpCount = maxJumpCount;
            takejumpcountOnce = true;
        }
        else
        {
            if(takejumpcountOnce == true)
            {
                JumpCount = JumpCount - 1;
                takejumpcountOnce = false;

            }
            MovingOnGround = false;
            playerAirTime += Time.deltaTime;
        }
    }
    
    void calculateMovement()
    {
        //Moves the player.
        //transform.position += new Vector3(movementVector.x, 0, movementVector.y) * Time.deltaTime * movementSpeed;

        //Moves the player based on camera direction.
        intendedDirection = camF * movementVector.y + camR * movementVector.x;
        ///magnitude is the amount of input. are we moving at all?
        if(movementVector.magnitude > 0)
        {
            //Lerps rotation speed and input to create a smooth rotation when turning.
            Quaternion rot = Quaternion.LookRotation(intendedDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }

        //Movement is only on the X and Y. Seperates the Y axis for gravity.
        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * movementVector.magnitude * movementSpeed, acceleration * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    void calculateGravity()
    {
        if (MovingOnGround == true)
        {
            //reset velocity.y. Keep the value low-ish so the player can move down slopes.
            velocity.y = -15f;
            resetGrav = true;
        }
        else
        {
            if (resetGrav == true)
            {
                velocity.y = -1f;
                resetGrav = false;
            }
            //begin moving the player down when they are not on the ground.
            velocity.y -= gravity * Time.deltaTime;
        }
        //Clamp the gravity so the player can't fall too fast.
        velocity.y = Mathf.Clamp(velocity.y, -50, Mathf.Infinity);
    }

    void calculateJump()
    {
        if ((playerAirTime < cyoteTime) || (JumpCount > 0))
        {
            //JumpCount = JumpCount - 1;
            resetGrav = false;
            playerAirTime = cyoteTime;
            jumpBufferTimer = 0f;
            velocity.y = jumpHeight;
        }
    }

    public void updateMovementSpeed(float speedChange)
    {
        movementSpeed *= speedChange;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && locked_on == false)
        {
            if (Input.GetButton("Lockon") && buffer == false)
            {
                locked_on = true;
                target = other.transform;
                buffer = true;
                lockedtext.enabled = true;
                timer = 1;
                
                    
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            locked_on = false;
            lockedtext.enabled = false;
            target = null;
        }
    }
}
