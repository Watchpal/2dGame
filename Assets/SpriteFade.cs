using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeSpeed = 2f;

    private bool isTriggered = false;

    void Update()
    {
        Color color = spriteRenderer.color;
        float targetAlpha = isTriggered ? 1f : 0f;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        spriteRenderer.color = color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}
