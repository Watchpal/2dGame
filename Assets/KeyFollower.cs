/*using UnityEngine;
using UnityEngine.InputSystem;

public class KeyFollower : MonoBehaviour
{
    private GameObject KeyObject;
    
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Key"))
        {
            KeyObject = other.gameObject;

            KeyObject.transform.SetParent(transform);

            KeyObject.transform.localPosition = new Vector3(-1f, 0f, 0f);

            KeyObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}*/