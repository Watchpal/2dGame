using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float parallaxFactor = 0.5f;

    private Vector3 startPosition;

        void Start()
        {
         
        startPosition=transform.position;
        }

    void LateUpdate()
    {
       float x =startPosition.x+cam.position.x*parallaxFactor;
       float y = startPosition.y + cam.position.y * parallaxFactor;
       transform.position = new Vector3(x, y, startPosition.z);


    }
}
 
