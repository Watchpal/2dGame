using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float fallGravity = 4f;
    public float jumpGravity = 3f;
    public float acceleration = 0.01f;      // How quickly to reach target speed
    public float deceleration = 0.04f;      // How quickly to stop when no input

    public float coyoteTime = 0f;          //Jump buffering and coyote time variables
    public float jumpBufferTime = 0f;
    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;
    private bool hasJumped;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.0005f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
        if (isGrounded && Mathf.Abs(rb.linearVelocity.y) <= 0.001f) 
        {
            coyoteTimeCounter = coyoteTime;
            hasJumped = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        
        jumpBufferTimeCounter-= Time.deltaTime;
        //Debug.Log(coyoteTimeCounter);

        if (jumpBufferTimeCounter>0 && coyoteTimeCounter>0 && !hasJumped)
        {
            rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );
            coyoteTimeCounter = 0;
            jumpBufferTimeCounter = 0;
            hasJumped = true;

        }
    }

    private void SetGravityMode()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallGravity;
        }
        else
        {
            rb.gravityScale = jumpGravity;
        }
    }


    private void FixedUpdate()
    {
        SetGravityMode();
        float targetSpeed = moveInput.x * moveSpeed;

        float newSpeed = Mathf.MoveTowards(
            rb.linearVelocity.x,
            targetSpeed,
            acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(
            newSpeed,
            rb.linearVelocity.y
        );
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpBufferTimeCounter = jumpBufferTime;
        }
        

        if (context.canceled &&
            rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * 0.5f
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
    public Vector2 Velocity => rb.linearVelocity;
    public bool IsGrounded => isGrounded;
    public float MoveInputX => moveInput.x;
}