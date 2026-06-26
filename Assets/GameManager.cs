using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxLives = 3;
    public int currentLives;
    public GameObject[] hearts;

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
        //hearts.UpdateHearts(currentLives);
        currentCheckpoint = startPosition;
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        currentCheckpoint = checkpointPos;
    }

    public void PlayerDied(GameObject player)
    {
        currentLives--;
        //hearts.UpdateHearts(currentLives);
        //hearts[currentLives].gameObject.SetActive(false);
        Debug.Log("player dies");

        
        player.transform.position = currentCheckpoint;
        /*else
        {
            hearts[2].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(true);
            hearts[0].gameObject.SetActive(true);
            currentLives = maxLives;
            currentCheckpoint = startPosition;
            player.transform.position = startPosition;
        
        }*/

        //make player drop dandelion
        CharacterMovement playerMovement =
    player.GetComponent<CharacterMovement>();

        playerMovement.ForceDropDandelion();

        //reset all dandelions
        foreach (Dandelion dandelion in FindObjectsByType<Dandelion>())
        {
            dandelion.Reset();
            
        }
       

        foreach (FallingPlatform platform in FindObjectsByType<FallingPlatform>())
        {
            platform.ResetPlatform();
        }

        foreach (PlatformSpriteSwap platformSprite in FindObjectsByType<PlatformSpriteSwap>())
        {
            platformSprite.ResetSprite();
        }
    }
}