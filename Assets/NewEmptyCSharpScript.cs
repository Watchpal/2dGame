using UnityEngine;

public class Player : MonoBehaviour

{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bur"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

