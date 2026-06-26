using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.InputSystem;

public class PlayerKey : MonoBehaviour
{
    public GameObject wall;
    public Transform keyFollowPoint;

    private bool hasKey = false;
    private GameObject KeyObject;
    private Vector3 keyVelocity = Vector3.zero;

    public Vector3 keyOffset = new Vector3(-0.1f, 0.1f, 0);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            KeyObject = other.gameObject;

            KeyObject.GetComponent<Collider2D>().enabled = false;

            FloatingKey floating = KeyObject.GetComponent<FloatingKey>();
            if (floating != null)
            {
                floating.enabled = false;
            }
            Debug.Log("Key collected!");
        }
}

    private void Update()
    {
        if (hasKey && KeyObject != null)
        {
           // Vector3 targetPos = transform.position + keyOffset;
            Vector3 targetPos = keyFollowPoint.position;

            Vector2 newPosition =
                Vector2.Lerp(
                    KeyObject.transform.position,
                    targetPos,
                    1f * Time.fixedDeltaTime
                );

            KeyObject.transform.position = newPosition;
        }

        if (hasKey && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            wall.SetActive(false);

            Destroy(KeyObject);

            hasKey = false;
        }
    }
}
