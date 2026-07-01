using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    
    private Vector3 lastPosition;
    private Vector3 startPosition;

        public Vector3 PlatformDelta { get; private set; }

    private void Start() 
    {
        startPosition = transform.position;
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        PlatformDelta = transform.position - lastPosition;
        lastPosition = transform.position;
    }
    public void ResetPlatform()
    {
        transform.position = startPosition;
    }
}
