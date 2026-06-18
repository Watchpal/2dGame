using UnityEngine;
public class script : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.startPosition = transform.position;
        GameManager.Instance.currentCheckpoint = transform.position;
    }

    
}
 