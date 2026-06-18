using UnityEngine;

public class dialognära : MonoBehaviour
{

    public GameObject dialogueText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueText.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            dialogueText.SetActive(false);
        }
    }
}