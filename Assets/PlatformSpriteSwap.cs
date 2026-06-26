using UnityEngine;
using System.Collections;

public class PlatformSpriteSwap : MonoBehaviour
{
    public Sprite newSprite;
    public float delay = 1f;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private bool isSwapping = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSwapping)
        {
            StartCoroutine(SwapSprite());
        }
    }

    IEnumerator SwapSprite()
    {
        isSwapping = true;
            spriteRenderer.sprite = newSprite;

        yield return new WaitForSeconds(delay);

            spriteRenderer.sprite = originalSprite;

        isSwapping = false;
    }

    public void ResetSprite()
    {
        StopAllCoroutines();
        spriteRenderer.sprite = originalSprite;
        isSwapping = false;
    }
}
