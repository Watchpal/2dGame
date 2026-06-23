using UnityEngine;
using System.Collections;
public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    public float respawnTime = 3f;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private bool triggered;

    private void awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(FallAndRespawn());
        }
    }

    private IEnumerator FallAndRespawn()
    {
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(respawnTime);

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPos;
        rb.bodyType = RigidbodyType2D.Kinematic;
        triggered = false;
    }
}


