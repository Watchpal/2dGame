using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxLives = 2;
    public int currentLives;
    public Hearts heartsUI;

    public Vector3 startPosition;
    public Vector3 currentCheckpoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentLives = maxLives;
        heartsUI.UpdateHearts(currentLives);
        currentCheckpoint = startPosition;
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        currentCheckpoint = checkpointPos;
    }

    public void TakeDamage(GameObject player)
    {
        currentLives--;
        heartsUI.UpdateHearts(currentLives);
        

        if (currentLives <= 0)
        {
            RespawnPlayer(player);
        }
    }
    public void PlayerDied(GameObject player)
    {
        RespawnPlayer(player);
   
    }
    public void RespawnPlayer(GameObject player)
    {
        currentLives=2;
        heartsUI.UpdateHearts(currentLives);
        
        

        
        player.transform.position = currentCheckpoint;
        

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