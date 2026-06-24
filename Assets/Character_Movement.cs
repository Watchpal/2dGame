using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 15f;
    public float fallGravity = 3.5f;
    public float jumpGravity = 2.75f;
    public float acceleration = 20f;
    public float maxFallSpeed = 7f;

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
    private Vector2 moveInput;

    private bool isGrounded;
    private bool hasJumped;

    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;

    [Header("Wall Check")]
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private float wallCheckRadius = 0.05f;
    [SerializeField] private LayerMask wallLayer;

    private bool touchingLeftWall;
    private bool touchingRightWall;

    [Header("Wall Jump")]
    public bool hasWallJump;
    [SerializeField] private float wallJumpX = 8f;
    [SerializeField] private float wallJumpY = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateGroundedState();
        HandleWallSliding();
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

    private void HandleWallSliding()
    {
        if (!hasWallJump)
            return;

        touchingLeftWall =
    Physics2D.OverlapCircle(
        wallCheckLeft.position,
        wallCheckRadius,
        wallLayer
    );

        touchingRightWall =
            Physics2D.OverlapCircle(
                wallCheckRight.position,
                wallCheckRadius,
                wallLayer
            );
        bool pressingIntoLeftWall =
    touchingLeftWall &&
    moveInput.x < -0.1f;

        bool pressingIntoRightWall =
            touchingRightWall &&
            moveInput.x > 0.1f;

        bool wallSliding =
            !isGrounded &&
            rb.linearVelocity.y < 0 &&
            (
                pressingIntoLeftWall ||
                pressingIntoRightWall
            );
        if (wallSliding)
        {
            rb.linearVelocity =
                new Vector2(
                    rb.linearVelocity.x,
                    -2f
                );
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

        if (carriedDandelion != null&&rb.linearVelocity.y<0)
        {
            rb.gravityScale = fallGravity / 10;
        }
        Mathf.Clamp(rb.linearVelocityY, -maxFallSpeed, maxFallSpeed);
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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput =
            context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isGrounded&&hasWallJump)
            {
                if (touchingLeftWall)
                {
                    rb.linearVelocity =
                        new Vector2(
                            wallJumpX,
                            wallJumpY
                        );

                    return;
                }

                if (touchingRightWall)
                {
                    rb.linearVelocity =
                        new Vector2(
                            -wallJumpX,
                            wallJumpY
                        );

                    return;
                }
            }
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
                1.5f,
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

        Vector2 throwDirection =
        moveInput.normalized;

        Vector2 throwVelocity =
            rb.linearVelocity +
            throwDirection * 6f;
        throwVelocity.y = throwVelocity.y / 2;

        carriedDandelion.Drop(throwVelocity);
        carriedDandelion = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            0.8f
        );
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