using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxLives = 3;
    public int currentLives;
    public Hearts hearts;

    public Vector3 startPosition;
    public Vector3 currentCheckpoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentLives = maxLives;
        hearts.UpdateHearts(currentLives);
        currentCheckpoint = startPosition;
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        currentCheckpoint = checkpointPos;
    }

    public void PlayerDied(GameObject player)
    {
        currentLives--;hearts.UpdateHearts(currentLives);

        if (currentLives > 0)
        {
            player.transform.position = currentCheckpoint;
        }
        else
        {
            currentLives = maxLives;
            currentCheckpoint = startPosition;
            player.transform.position = startPosition;
        
        }
    }
}