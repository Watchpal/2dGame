using UnityEngine;

public class dörespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<checkpoint>().Respawn();  
        }
    }
}
