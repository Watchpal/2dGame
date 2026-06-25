using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKey : MonoBehaviour
{
    public GameObject wall;

    private bool hasKey = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            Debug.Log("Key collected!");
        }
}

    private void Update()
    {
        if (hasKey && Keyboard.current.eKey.wasPressedThisFrame)

        {
            wall.SetActive(false);
        }
    }
}
