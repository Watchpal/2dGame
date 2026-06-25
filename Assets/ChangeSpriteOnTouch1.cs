using UnityEngine;
using System.Collections;

public class ChangeSpriteOnTouch1 : MonoBehaviour
{
    public Sprite newSprite;
    public float delay = 1f;

    private SpriteRenderer sr;
    private Sprite originalSprite;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalSprite = sr.sprite; //Remember the starting sprite
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sr.sprite = newSprite;
            StartCoroutine(ReturnToOriginal());
        }
    }


    IEnumerator ReturnToOriginal()
    {
        yield return new WaitForSeconds(delay);
        sr.sprite = originalSprite;
    }    
}
            
            
