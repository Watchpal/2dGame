using UnityEngine;

public class DestroyMultipleObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDestroy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }
    }
}