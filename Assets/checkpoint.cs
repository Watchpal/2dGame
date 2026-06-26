using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite active;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite=idle;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated)
            return;

        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.SetCheckpoint(
            transform.position
        );

        activated = true;
        spriteRenderer.sprite = active;

        Debug.Log(
            "Checkpoint activated: " +
            transform.position
        );
    }
}