using UnityEngine;

public class coincolllector : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Coincounter.instance.AddCoin(value);
            Destroy(gameObject);
        }

    }
}
