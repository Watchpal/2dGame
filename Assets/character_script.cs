using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class character_script : MonoBehaviour
{
    public float forceAmount = 10f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Horizontal movement speed
    public float jumpForce = 10f;      // Jump strength

    [Header("Ground Check Settings")]
    public Transform groundCheck;      // Empty GameObject at feet position
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;      // Layer(s) considered as ground

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            // Apply an upward force
            rb.AddForce(Vector3.up * forceAmount, (ForceMode2D)ForceMode.Impulse);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            // Apply an upward force
            rb.AddForce(Vector3.left * forceAmount, (ForceMode2D)ForceMode.Impulse);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            // Apply an upward force
            rb.AddForce(Vector3.right * forceAmount, (ForceMode2D)ForceMode.Impulse);
        } 
         // Check if player is on the ground
         isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Keyboard.current.upArrowKey.wasPressedThisFrame &&isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Draw ground check radius in editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
