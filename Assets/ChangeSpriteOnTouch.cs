using UnityEngine;

public class ChangeSpriteOnTouch : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}
