using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform spawnPoint; 

    public void Respawn()
    {
        transform.position = spawnPoint.position;
    }
}