using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInvincibility inv = other.GetComponent<PlayerInvincibility>();

            if (!inv.IsInvincible)
            {
                GameManager.Instance.TakeDamage(other.gameObject);
                other.GetComponent<PlayerInvincibility>().ApplyKnockback();
                inv.TriggerInvincibility();
            }
        }
        
    }
}