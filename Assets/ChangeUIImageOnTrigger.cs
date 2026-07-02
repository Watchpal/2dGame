using UnityEngine;
using UnityEngine.UI;

public class ChangeUIImageOnTrigger : MonoBehaviour
{
    [Header ("UI")]
    public Image uiImage;

    [Header("Sprites")]
    public Sprite newSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
            uiImage.sprite = newSprite;
        }
    }
}
