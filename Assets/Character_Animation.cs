using UnityEngine;



public class Character_Animation : MonoBehaviour
{
    private Animator anim;
    private CharacterMovement movement;
    private SpriteRenderer spriteRenderer;
    private Transform visuals;

    private bool wasGrounded;
    private Vector3 targetScale = Vector3.one;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        movement = GetComponentInParent<CharacterMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        visuals = transform;

    }
    private void StartLandingSquash()
    {
        targetScale = new Vector3(
            1.2f,
            0.8f,
            1f
        );
    }
    private void Update()
    {
        anim.SetFloat(
            "Speed",
            Mathf.Abs(movement.Velocity.x)
        );

        anim.SetBool(
            "IsGrounded",
            movement.IsGrounded
        );

        anim.SetFloat(
            "YVelocity",
            movement.Velocity.y
        );

        //flips in the correct direction
        if (movement.MoveInputX > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.MoveInputX < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        

        //below is dynamic squash and stretch
        if (!wasGrounded && movement.IsGrounded)
        {
            StartLandingSquash();
        }

        wasGrounded = movement.IsGrounded;


        visuals.localScale = Vector3.Lerp(
        visuals.localScale,
        targetScale,
        20f * Time.deltaTime
    );

        targetScale = Vector3.one;
    }
}
