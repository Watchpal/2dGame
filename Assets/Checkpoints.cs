using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpoint(transform.position);
            Debug.Log("Checkpoint reached!");
        }
    }
}