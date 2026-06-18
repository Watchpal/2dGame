using UnityEngine;

public class dandelion : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool IsCarried { get; private set; }
    private Transform followTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("GroundLayer");
    }

    private void FixedUpdate()
    {
        if (IsCarried)
        {
            Vector2 direction =
            followTarget.position - transform.position;

            rb.AddForce(direction * 25);
        }
    }
    public void PickUp(Transform carryPoint)
    { IsCarried = true;
      followTarget = carryPoint;
      gameObject.layer = LayerMask.NameToLayer("CarriedDandelion");
    }

    public void Drop()
    {
        IsCarried = false;
        followTarget = null;
        gameObject.layer = LayerMask.NameToLayer("GroundLayer");
    }
}