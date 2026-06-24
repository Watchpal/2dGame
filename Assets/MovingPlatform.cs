using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   private Vector3 lastPosition;

        public Vector3 PlatformDelta { get; private set; }

    private void Start() 
    {
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        PlatformDelta = transform.position - lastPosition;
        lastPosition = transform.position;
    }
}
