using UnityEngine;

public class dandelion : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool IsCarried { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PickUp(Transform carryPoint)
    {
        IsCarried = true;

        rb.simulated = false;

        transform.SetParent(carryPoint);
        transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        IsCarried = false;

        transform.SetParent(null);

        rb.simulated = true;
    }
}