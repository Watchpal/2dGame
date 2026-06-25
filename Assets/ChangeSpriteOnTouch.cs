using UnityEngine;

public class ChangeSpriteOnTouch : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          sr.sprite = newSprite;
        }
    }
}
