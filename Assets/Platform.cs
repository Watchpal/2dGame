using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private bool PlayerOnPlatform = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isrunning", true);
    }

    // Updat is called once per frame
    void Update()
    {
        if (!PlayerOnPlatform)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        
        Vector2 dir = (currentPoint.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            if (currentPoint == pointA.transform)
                currentPoint = pointB.transform;
            else
                currentPoint = pointA.transform;
    }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f
           && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
        
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5 && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5 && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOnPlatform = true;
        }
    }
    
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    
    /*private void OnDrawGizmos()
    {
        if (pointA == null || pointB == null)
            return; // Prevent error in edit mode
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }*/
}


