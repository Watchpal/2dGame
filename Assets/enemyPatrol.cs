using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
        public GameObject pointA;
        public GameObject pointB;
        private Rigidbody2D rb; 
        private Animator anim;
        private Transform currentPoint;
        public float speed;
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
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        ;

        if(Vector3.Distance(transform.position, currentPoint.position) < 0.5 && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5 && currentPoint == pointA.transform)
        {
                currentPoint = pointB.transform;
        }
    }
}
