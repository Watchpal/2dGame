using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement1 : MonoBehaviour
{
    public string sceneToLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Fabrik");
        }
    }
}
