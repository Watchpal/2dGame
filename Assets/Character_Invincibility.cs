using System.Collections;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration = 0.25f;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Animator animator;
    [SerializeField] private float knockbackForce = 50f;
    [SerializeField] private float knockbackUpForce = 5f;

    private Rigidbody2D rb;

    public bool IsInvincible { get; private set; }
    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }
    public void TriggerInvincibility()
    {
        if (!IsInvincible)
            StartCoroutine(InvincibilityRoutine());
    }

    private IEnumerator InvincibilityRoutine()
    {
        IsInvincible = true;

        float timer = 0f;

        while (timer < invincibilityDuration)
        {
            animator.enabled = !animator.enabled;

            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval;
        }

        animator.enabled = true;
        IsInvincible = false;
    }


    public void ApplyKnockback()
    {
        Vector2 direction;
        direction.x = -rb.linearVelocityX;
        direction.y = 0f;
        direction.Normalize();

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(
            direction * knockbackForce + Vector2.up * knockbackUpForce,
            ForceMode2D.Impulse);
    }
}


