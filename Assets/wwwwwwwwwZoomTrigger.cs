using UnityEngine;

public class wwwwwwwwwZoomTrigger : MonoBehaviour
{
    public CameraZoomOut cameraZoom;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered!");

        if (other.CompareTag("Player"))
        {
            cameraZoom.ZoomOut();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraZoom.ZoomIn();
        }
    }
}