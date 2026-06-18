using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3;

    private Vector2 startPosition;
    private Vector2 checkpointPosition;

    private void Start()
    {
        startPosition = transform.position;
        checkpointPosition = startPosition;
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public void die()
    {
        lives--;

        if (lives > 0)
        {
            transform.position = checkpointPosition;
        }
        else
        {
            lives = 3;
            checkpointPosition = startPosition;
            transform.position = startPosition;
        }

        Debug.Log("Lives: " + lives);
    }
}