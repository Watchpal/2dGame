using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float parallaxFactor = 0.5f;

    private float previousCamX;

        void Start()
        {
         previousCamX = cam.position.x;
        }

    void LateUpdate()
    {
       float deltaX = cam.position.x - previousCamX;
       transform.position += Vector3.right * (deltaX * parallaxFactor);

        previousCamX = cam.position.x;
    }
}
 
