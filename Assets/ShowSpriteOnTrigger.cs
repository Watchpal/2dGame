using UnityEngine;

public class ShowSpriteOnTrigger : MonoBehaviour
{
    public GameObject[] spriteToShow;
    private void Start()
    {
        //spriteToShow.SetActive(false); // Hide at start
        foreach (GameObject sprite in spriteToShow)
        {
            sprite.GetComponent<SpriteRenderer>().enabled = false;
        }
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //spriteToShow.SetActive(true);
            foreach (GameObject sprite in spriteToShow)
            {

                sprite.GetComponent<SpriteRenderer>().enabled = true;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //spriteToShow.SetActive(true);
            foreach (GameObject sprite in spriteToShow)
            {

                sprite.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}

