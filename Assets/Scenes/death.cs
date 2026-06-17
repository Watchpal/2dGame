using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class death : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            other.transform.position = spawnPoint.position;
        }
    }

   
}
