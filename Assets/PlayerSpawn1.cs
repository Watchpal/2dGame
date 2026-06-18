using UnityEngine;

public class PlayerSpawn1 : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.startPosition = transform.position;
        GameManager.Instance.currentCheckpoint = transform.position;
    }
}