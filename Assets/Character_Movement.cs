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
    [SerializeField] private float normalMass = 1f;
    [SerializeField] private float dandelionMass = 0.2f;
    private Dandelion highlightedDandelion;

    private int dandelionsSupportingPlayer = 0;

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
    [SerializeField]
    private Vector2 wallCheckSize =
    new Vector2(0.1f, 0.8f);
    [SerializeField] private LayerMask wallLayer;

    private bool touchingLeftWall;
    private bool touchingRightWall;
    private bool wallSliding;

    [Header("Wall Jump")]
    public bool hasWallJump;
    [SerializeField] private float wallJumpX = 8f;
    [SerializeField] private float wallJumpY = 10f;
    public bool FacingRight { get; private set; } = true;

    private bool movementEnabled = true;

    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;

        if (!enabled)
            rb.linearVelocity = Vector2.zero;
    }
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
        UpdatePickupHighlight();
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

        if (isGrounded)
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
    Physics2D.OverlapBox(
        wallCheckLeft.position,
        wallCheckSize,
        0f,
        wallLayer
    );

        touchingRightWall =
            Physics2D.OverlapBox(
                wallCheckRight.position,
                wallCheckSize,
                0f,
                wallLayer
            );
        bool pressingIntoLeftWall =
    touchingLeftWall &&
    moveInput.x < -0.1f;

        bool pressingIntoRightWall =
            touchingRightWall &&
            moveInput.x > 0.1f;

         wallSliding =
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
        if (!movementEnabled) //only executes when movement is enabled
            { return; }
        if (jumpBufferTimeCounter <= 0)
            return;
        if (!isGrounded && hasWallJump)
        {
            if (touchingLeftWall)
            {
                rb.linearVelocity =
                    new Vector2(
                        wallJumpX,
                        wallJumpY
                    );
                FacingRight = true;
                return;

            }

            if (touchingRightWall)
            {
                rb.linearVelocity =
                    new Vector2(
                        -wallJumpX,
                        wallJumpY
                    );
                FacingRight = false;
                return;
            }
        }
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
        if (!movementEnabled) //only executes when movement is enabled
        { return; }
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
        if (moveInput.x > 0.01f)
            FacingRight = true;
        else if (moveInput.x < -0.01f)
            FacingRight = false;
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

    private void UpdatePickupHighlight()
    {
        if (carriedDandelion != null)
        {
            if (highlightedDandelion != null)
            {
                highlightedDandelion.SetHighlighted(false);
                highlightedDandelion = null;
            }

            return;
        }

        Vector3 pickUpPos =
            transform.position + new Vector3(0f, 0.3f, 0f);

        Vector2 pickUpSize =
            new Vector2(2.9f, 2.4f);

        Collider2D[] hits =
            Physics2D.OverlapBoxAll(
                pickUpPos,
                pickUpSize,
                0f,
                dandelionLayer
            );

        Dandelion closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            Dandelion dandelion =
                hit.GetComponentInParent<Dandelion>();

            if (dandelion == null)
                continue;

            float distance =
                Vector2.Distance(
                    transform.position,
                    dandelion.Position
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = dandelion;
            }
        }

        if (closest == highlightedDandelion)
            return;

        if (highlightedDandelion != null)
            highlightedDandelion.SetHighlighted(false);

        highlightedDandelion = closest;

        if (highlightedDandelion != null)
            highlightedDandelion.SetHighlighted(true);
    }

    private void TryPickup()
    {
        if (highlightedDandelion == null)
            return;

        carriedDandelion = highlightedDandelion;
        carriedDandelion.PickUp(carryPoint);

        highlightedDandelion.SetHighlighted(false);
        highlightedDandelion = null;
    }

    private void DropDandelion()
    {
        if (carriedDandelion == null)
            return;

        Vector2 throwVelocity;

        if (moveInput.y < -0.5f)
        {
            throwVelocity = Vector2.zero;
        }
        else
        {
            Vector2 throwDirection;

            if (moveInput.sqrMagnitude < 0.01f)
            {
                throwDirection = FacingRight ? Vector2.right : Vector2.left;
            }
            else
            {
                throwDirection = moveInput.normalized;
            }

            throwVelocity =
                rb.linearVelocity +
                throwDirection * 9f;

            throwVelocity.y *= 0.5f;
        }

        carriedDandelion.Drop(throwVelocity);
        carriedDandelion = null;
    }

    public void ForceDropDandelion()
    {
        if (carriedDandelion == null)
            return;

        carriedDandelion.Drop(Vector2.zero);
        carriedDandelion = null;
    }
    public void EnterDandelionPlatform()
    {
        dandelionsSupportingPlayer++;

        if (dandelionsSupportingPlayer == 1)
        {
            rb.mass = dandelionMass;
        }
    }

    public void ExitDandelionPlatform()
    {
        dandelionsSupportingPlayer =
            Mathf.Max(0, dandelionsSupportingPlayer - 1);

        if (dandelionsSupportingPlayer == 0)
        {
            rb.mass = normalMass;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 pickUpPos = transform.position + new Vector3(0f, 0.3f, 0f);
        Vector2 pickUpSize = new Vector2(2.9f, 2.4f);
        Gizmos.DrawWireCube(
            pickUpPos,
            pickUpSize
        );

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(
      wallCheckLeft.position,
      wallCheckSize
  );
        Gizmos.DrawWireCube(
      wallCheckRight.position,
      wallCheckSize
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

    public bool WallSliding =>
        wallSliding;

    public float MoveInputX =>
        moveInput.x;
}