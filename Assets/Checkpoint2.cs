using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    public GameObject checkPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
         {
            checkPoint.gameObject.GetComponent<checkpoint>().respawnPoint = transform.position;
         }

    }
}
