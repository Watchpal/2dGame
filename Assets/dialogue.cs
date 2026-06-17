using UnityEngine;
using UnityEngine.InputSystem;

public class dialogue : MonoBehaviour
{
    public GameObject dialogueText;
    private bool playerNearby;

    void Update()
    {
        /*
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            dialogueText.SetActive(!dialogueText.activeSelf);
        }
        */
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            dialogueText.SetActive(false);
        }
    }

    public void OnTalk(InputAction.CallbackContext context)
    {
        if(context.started && playerNearby)
        {
            dialogueText.SetActive(true);
            Debug.Log("talk");
        }
    }

}
