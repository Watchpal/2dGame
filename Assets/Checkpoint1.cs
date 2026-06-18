using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
       {
            Currentcheckpoint player = other.GetComponent<Currentcheckpoint>();
            player.SetCheckpoint(transform.position);
       }
    }
}
