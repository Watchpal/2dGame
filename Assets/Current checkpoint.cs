using UnityEngine;

public class Currentcheckpoint : MonoBehaviour
{
    public Vector2 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position; //default spawn  
    }

    public void SetCheckpoint(Vector2 newPoint)
    {
        spawnPoint = newPoint;
    }

    public void Respawn()
    {
        transform.position = spawnPoint;
    }
}
