using UnityEngine;



public class Character_Animation : MonoBehaviour
{
    private Animator anim;
    private CharacterMovement movement;
    private SpriteRenderer spriteRenderer;
    private Transform visuals;

    private bool wasGrounded;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        movement = GetComponentInParent<CharacterMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        visuals = transform;

    }

    private void Update()
    {
        anim.SetFloat(
            "Speed",
            Mathf.Abs(movement.Velocity.x)
        );

        anim.SetBool(
            "isGrounded",
            movement.IsGrounded
        );

        anim.SetBool(
            "wallSliding",
            movement.WallSliding
        );

        anim.SetFloat(
            "yVelocity",
            movement.Velocity.y
        );

        //changes walk animation speed depending on velocity
        float speedMultiplier =
    Mathf.Clamp(
        Mathf.Abs(movement.Velocity.x) / movement.moveSpeed,
        0f,
        3f
    );

        anim.SetFloat("walkAnimationSpeed", speedMultiplier);

        //flips in the correct direction
        spriteRenderer.flipX = !movement.FacingRight;

        //trigger squash upon landing
        if (!wasGrounded && movement.IsGrounded)
        {
            anim.SetTrigger("Land");
        }

        wasGrounded = movement.IsGrounded;


    }
}