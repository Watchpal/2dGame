/*using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Dandelion")]
    [SerializeField] private Transform carryPoint;
    [SerializeField] private LayerMask dandelionLayer;


    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float fallGravity = 4f;
    public float jumpGravity = 3f;
    public float acceleration = 15f;

    [Header("Jump Assist")]
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;
    private bool hasJumped;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.05f;
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
        UpdateGroundedState();
        UpdateJumpTimers();
        HandleBufferedJump();
    }

    private void FixedUpdate()
    { 
        HandleGravity();
        HandleMovement();
    }

    private void UpdateGroundedState()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (isGrounded &&
            Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            coyoteTimeCounter = coyoteTime;
            hasJumped = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        
    }

    private void UpdateJumpTimers()
    {
        jumpBufferTimeCounter -= Time.deltaTime;
    }

    private void HandleBufferedJump()
    {
        if (jumpBufferTimeCounter <= 0)
            return;

        if (coyoteTimeCounter <= 0)
            return;

        if (hasJumped)
            return;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );

        jumpBufferTimeCounter = 0;
        coyoteTimeCounter = 0;
        hasJumped = true;
    }

    

    private void HandleGravity()
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

    private void HandleMovement()
    {
        

        float targetSpeed =
            moveInput.x * moveSpeed;

        float newSpeed =
            Mathf.MoveTowards(
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
        moveInput =
            context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
            jumpBufferTimeCounter =
                jumpBufferTime;
        }

        if (context.canceled)
        {
            

            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity =
                    new Vector2(
                        rb.linearVelocity.x,
                        rb.linearVelocity.y * 0.5f
                    );
            }
        }
    }
/*
    public void OnInteract(
        InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (carriedDandelion == null)
        {
            TryPickup();
        }
        else
        {
            DropDandelion();
        }
    }

    private void TryPickup()
    {
        Collider2D hit =
            Physics2D.OverlapCircle(
                transform.position,
                1f,
                dandelionLayer
            );

        if (!hit)
            return;

        carriedDandelion =
            hit.GetComponentInParent<Dandelion>();

        if (carriedDandelion != null)
        {
            carriedDandelion.PickUp(
                carryPoint
            );
        }
    }

    private void DropDandelion()
    {
        if (carriedDandelion == null)
            return;

        dandelionJoint.enabled = false;

        carriedDandelion.Drop();
        carriedDandelion = null;
        rb.linearVelocityY += 4f;
        rb.linearVelocity *= 1.5f;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }

    public Vector2 Velocity =>
        rb.linearVelocity;

    public bool IsGrounded =>
        isGrounded;

    public float MoveInputX =>
        moveInput.x;
}
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float fallGravity = 4f;
    public float jumpGravity = 3f;
    public float acceleration = 15f;

    [Header("Jump Assist")]
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.05f;
    public LayerMask groundLayer;

    [Header("Dandelion")]
    public Transform carryPoint;
    public LayerMask dandelionLayer;

    private Dandelion carriedDandelion;

    private Rigidbody2D rb;
    private DistanceJoint2D swingJoint;

    private Vector2 moveInput;

    private bool isGrounded;
    private bool hasJumped;

    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        swingJoint = GetComponent<DistanceJoint2D>();
    }

    private void Update()
    {
        UpdateGroundedState();
        UpdateJumpTimers();
        HandleBufferedJump();
        UpdateSwingState();
    }

    private void FixedUpdate()
    {
        HandleGravity();
        HandleMovement();
    }

    private void UpdateGroundedState()
    {
        isGrounded =
            Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );

        if (isGrounded &&
            Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            coyoteTimeCounter = coyoteTime;
            hasJumped = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void UpdateJumpTimers()
    {
        jumpBufferTimeCounter -= Time.deltaTime;
    }

    private void HandleBufferedJump()
    {
        if (jumpBufferTimeCounter <= 0)
            return;

        if (coyoteTimeCounter <= 0)
            return;

        if (hasJumped)
            return;

        rb.linearVelocity =
            new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

        jumpBufferTimeCounter = 0;
        coyoteTimeCounter = 0;
        hasJumped = true;
    }

    private void HandleGravity()
    {
        if (rb.linearVelocity.y < 0)
            rb.gravityScale = fallGravity;
        else
            rb.gravityScale = jumpGravity;
    }

    private void HandleMovement()
    {
        float targetSpeed =
            moveInput.x * moveSpeed;

        float newSpeed =
            Mathf.MoveTowards(
                rb.linearVelocity.x,
                targetSpeed,
                acceleration * Time.fixedDeltaTime
            );

        rb.linearVelocity =
            new Vector2(
                newSpeed,
                rb.linearVelocity.y
            );
    }

    private void UpdateSwingState()
    {
        if (carriedDandelion == null)
            return;

        bool shouldSwing =
            !isGrounded &&
            rb.linearVelocity.y < 0f;

        if (shouldSwing)
        {
            if (!swingJoint.enabled)
            {
                swingJoint.connectedBody =
                    carriedDandelion.StemTipRb;

                swingJoint.distance = 0.6f;
                swingJoint.enabled = true;

                carriedDandelion.SetGroundFollow(false);
            }
        }
        else
        {
            if (swingJoint.enabled)
            {
                swingJoint.enabled = false;
                carriedDandelion.SetGroundFollow(true);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput =
            context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpBufferTimeCounter =
                jumpBufferTime;
        }

        if (context.canceled &&
            rb.linearVelocity.y > 0)
        {
            rb.linearVelocity =
                new Vector2(
                    rb.linearVelocity.x,
                    rb.linearVelocity.y * 0.5f
                );
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (carriedDandelion == null)
            TryPickup();
        else
            DropDandelion();
    }

    private void TryPickup()
    {
        Collider2D hit =
            Physics2D.OverlapCircle(
                transform.position,
                1f,
                dandelionLayer
            );

        if (!hit)
            return;

        carriedDandelion =
            hit.GetComponentInParent<Dandelion>();

        if (carriedDandelion != null)
        {
            carriedDandelion.PickUp(carryPoint);
        }
    }

    private void DropDandelion()
    {
        if (carriedDandelion == null)
            return;

        swingJoint.enabled = false;

        carriedDandelion.Drop();
        carriedDandelion = null;
    }
    public Vector2 Velocity =>
        rb.linearVelocity;

    public bool IsGrounded =>
        isGrounded;

    public float MoveInputX =>
        moveInput.x;
}