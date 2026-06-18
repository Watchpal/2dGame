using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.startPosition = transform.position;
        GameManager.Instance.currentCheckpoint = transform.position;
    }
}