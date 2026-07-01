using UnityEngine;

public class PlatformPassenger : MonoBehaviour
{
    private MovingPlatform platform;

   

    private void LateUpdate()
    {
        if (platform != null)
        {
            transform.position += platform.PlatformDelta;
        }
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        platform = collision.gameObject.GetComponent<MovingPlatform>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>() == platform)
        {
            platform = null;
        }
    }
   
}