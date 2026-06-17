using UnityEngine;

public class dandelion : MonoBehaviour
{
    public bool IsCarried { get; private set; }

    public void PickUp(Transform CarryPoint)
    {
        IsCarried = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.simulated = false;

        transform.SetParent(CarryPoint);
        transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        IsCarried = false;

        transform.SetParent(null);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.simulated = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
