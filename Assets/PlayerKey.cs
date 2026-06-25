using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKey : MonoBehaviour
{
    public GameObject wall;

    private bool hasKey = false;
    private GameObject KeyObject;

    public Vector3 keyOffset = new Vector3(0.7f, 0.5f, 0);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Debug.Log("Key collected!");
        }
}

    private void Update()
    {
        if (hasKey && Keyboard.current.eKey.wasPressedThisFrame)
        {
            wall.SetActive(false);

            Destroy(KeyObject);

            hasKey = false;
        }
    }
}
