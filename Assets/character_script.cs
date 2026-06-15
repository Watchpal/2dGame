using UnityEngine;
using UnityEngine.InputSystem;

public class character_script : MonoBehaviour
{
    Rigidbody2D rb;
    public float forceAmount = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            // Apply an upward force
            rb.AddForce(Vector3.up * forceAmount, (ForceMode2D)ForceMode.Impulse);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            // Apply an upward force
            rb.AddForce(Vector3.left * forceAmount, (ForceMode2D)ForceMode.Impulse);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            // Apply an upward force
            rb.AddForce(Vector3.right * forceAmount, (ForceMode2D)ForceMode.Impulse);
        }
    }
}
