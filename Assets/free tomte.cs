using UnityEngine;

public class freetomte : MonoBehaviour
{
    public Animator animator;

        private void OnCollisionEnter(Collision collision)
    {
        //animator.SetTrigger("Touch");
        //animator.Play("Free tomte");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //animator.Play("Free tomte");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            animator.Play("Free tomte");
        }
    }


    private void Start()
    {
        //animator.Play("Free tomte");
    }
}
