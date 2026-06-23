using UnityEngine;

public class ShowImageOnTrigger : MonoBehaviour
{
    public GameObject imageToShow;
    public float displayTime = 3f;

    private SpriteRenderer SpriteRenderer;

        void Start() 
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (imageToShow != null)
            imageToShow.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

}
