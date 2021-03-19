using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 10.0f;
    [SerializeField] [Range(0.0f, 1.0f)] float airSpeedMultiplier = 0.4f;
    [SerializeField] float maxHorizontalSpeed = 10.0f;
    [SerializeField] float groundSpeedMultiplier = 10.0f;
    [SerializeField] float groundDrag = 6.0f;

    // Parameter from old movement system, not used now
    //[SerializeField][Range(0.0f, 0.5f)] 
    float moveSmoothTime = 0.3f;

    [Header("Jumping")]
    [SerializeField] float playerGravity = 20.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float airDrag = 0.2f;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDistance = 0.4f;

    Rigidbody rb = null;

    Vector2 currentHorDir = Vector2.zero;
    Vector2 currentHorDirVelocity = Vector2.zero;
    Vector3 slopeMoveDir = Vector3.one;
    
    float xDirection;
    float yDirection;
    Vector3 moveDirection = Vector3.zero;

    bool jumpInput = false;
    bool isPaused = false;

    RaycastHit slopeHit;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        pauseDetect();
        if (!isPaused)
        {
            CheckForGround();
            DragControl();
            setSlopeMoveDir();
            UpdateJumpInput();
        }
        //Debug.Log(OnSlope());
    }

    private void FixedUpdate()
    {
        //updateMovement();
        if (!isPaused)
        {
            addPlayerGravity();
            jumpHandling();
            ForceMovement();
        }
    }

    // Old movement system
    // Velocity based
    void updateMovement()
    {
        Vector2 targetHorDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetHorDir.Normalize();
        
        currentHorDir = Vector2.SmoothDamp(currentHorDir, targetHorDir, ref currentHorDirVelocity, moveSmoothTime);

        Vector3 playerVelocity = (transform.forward * currentHorDir.y + transform.right * currentHorDir.x) * moveSpeed;

        if (OnSlope())
        {
            rb.velocity = new Vector3(playerVelocity.x, rb.velocity.y, playerVelocity.z).magnitude * slopeMoveDir.normalized;
        }
        else
        {
            rb.velocity = new Vector3(playerVelocity.x, rb.velocity.y, playerVelocity.z);
        }       
        
    }

    void jumpHandling()
    {
        if (jumpInput)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInput = false;
        }
    }

    void ForceMovement()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        yDirection = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * yDirection + transform.right * xDirection;
        moveDirection.Normalize();

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection * moveSpeed * groundSpeedMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDir * moveSpeed * groundSpeedMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection * moveSpeed * groundSpeedMultiplier * airSpeedMultiplier, ForceMode.Acceleration);
        }
        
        Vector2 horizontalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        horizontalVelocity = Vector2.ClampMagnitude(horizontalVelocity, maxHorizontalSpeed);
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.y);
        //Debug.Log(horizontalVelocity.magnitude);
    }

    void DragControl()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void UpdateJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpInput = true;
        }
    }

    void CheckForGround()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundLayer.value);
    }

    bool OnSlope()
    {
        //Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.green);
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1f + 0.2f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void setSlopeMoveDir()
    {
        // Old Slope Movement
        //slopeMoveDir = Vector3.ProjectOnPlane(rb.velocity.normalized, slopeHit.normal);

        slopeMoveDir = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void addPlayerGravity()
    {
        rb.AddForce(Vector3.down * playerGravity, ForceMode.Acceleration);
    }

    void pauseDetect()
    {
        if (Time.timeScale == 1.0f)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
}
