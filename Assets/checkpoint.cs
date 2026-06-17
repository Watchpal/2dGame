using UnityEngine;

public class checkpoint : MonoBehaviour
{
   public Vector2 respawnPoint;

        void Start()
    {
        respawnPoint = transform.position;
    }

    public void SetCheckpoint(Vector2 newPoint)
    {
        respawnPoint = newPoint;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
    }
}