using System.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cutsceneCamera;

    public float cameraTime = 3f;

    public MonoBehaviour playerMovement;

    private bool hasBeenTriggerd = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasBeenTriggerd)
            return;

        if (other.CompareTag("Player"))
        {
            hasBeenTriggerd = true;
            StartCoroutine(CameraSequence());
        }
}

    IEnumerator CameraSequence()
    {
      //  playerMovement.enabled = false;
        
        mainCamera.SetActive(false);
        cutsceneCamera.SetActive(true);

        yield return new WaitForSeconds(cameraTime);

        cutsceneCamera.SetActive(false);
        mainCamera.SetActive(true);

        Destroy(gameObject);
    }
}