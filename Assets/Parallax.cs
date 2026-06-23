using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float parallaxFactor = 0.5f;

    private Vector3 startPosition;
    private Vector3 cameraStartPosition;

        void Start()
        {
         
        startPosition=transform.position;
        cameraStartPosition = cam.position;
        }

    void LateUpdate()
    {
        Vector3 offset =
         (Camera.main.transform.position - cameraStartPosition)
         * parallaxFactor;

        transform.position = new Vector3(
     startPosition.x +
     (cam.position.x - cameraStartPosition.x) * parallaxFactor,
     startPosition.y,
     startPosition.z
 );

    }
}
 
