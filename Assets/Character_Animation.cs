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

        //changes walk animation speed depending on velocity
        float speedMultiplier =
    Mathf.Clamp(
        Mathf.Abs(movement.Velocity.x) / movement.moveSpeed,
        0f,
        3f
    );

        anim.SetFloat("walkAnimationSpeed", speedMultiplier);

        //flips in the correct direction
        if (movement.MoveInputX > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.MoveInputX < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        

        
    }
}
