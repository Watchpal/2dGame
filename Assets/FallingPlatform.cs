using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    public float respawnTime = 3f;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private Quaternion startRotation;
    private bool triggered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        startRotation = transform.rotation;
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

        //Freeze the platform
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Vector3 currentPos = transform.position;
        Quaternion currentRot = transform.rotation;

        float duration = 1f; //Time to move back
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(currentPos, startPos, t);
            transform.rotation = Quaternion.Lerp(currentRot, startRotation, t);
               
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
        transform.rotation = startRotation;

        triggered = false;

        

    }

    public void ResetPlatform()
    {
        StopAllCoroutines();

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        transform.position = startPos;
        transform.rotation = startRotation;

        triggered = false;
    }
}


