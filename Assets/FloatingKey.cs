using UnityEngine;

public class FloatingKey : MonoBehaviour
{
   private Vector3 startPos;

    public float height = 0.15f;
    public float speed = 2f;

    void Start() 
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition =
            startPos + Vector3.up * Mathf.Sin(Time.time * speed) * height;
    }
}
