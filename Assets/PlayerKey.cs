using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKey : MonoBehaviour
{
    public GameObject wall;

    private bool hasKey = false;
    private GameObject KeyObject;

    public Vector3 keyOffset = new Vector3(-0.1f, 0.1f, 0);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            KeyObject = other.gameObject;

            KeyObject.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Key collected!");
        }
}

    private void Update()
    {
        if (hasKey && KeyObject != null)
        {
            Vector3 targetPos = transform.position + keyOffset;

            KeyObject.transform.position = Vector3.Lerp(
                KeyObject.transform.position,
                targetPos,
                6f * Time.deltaTime
                );
        }

        if (hasKey && Keyboard.current.eKey.wasPressedThisFrame)
        {
            wall.SetActive(false);

            Destroy(KeyObject);

            hasKey = false;
        }
    }
}
