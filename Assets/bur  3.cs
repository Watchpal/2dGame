using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class bur3 : MonoBehaviour
{
    private Animator anim;
    private bool isFree=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")&&!isFree)
        {
            //other.gameObject.SetActive(false);
            //Coincounter.instance.AddCoin(1); //doesn't work in factory for some reason
            isFree = true;
            anim.SetTrigger("isFree");

        }
    }
}
