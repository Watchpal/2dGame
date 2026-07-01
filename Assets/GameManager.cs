using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxLives = 2;
    public int currentLives;
    public Hearts heartsUI;

    public Vector3 startPosition;
    public Vector3 currentCheckpoint;

     private ScreenWipe screenWipe;
    private CinemachineCamera cinemachineCamera;
    
    private CinemachineConfiner2D confiner;

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
        screenWipe = FindAnyObjectByType<ScreenWipe>();
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        confiner = FindAnyObjectByType<CinemachineConfiner2D>();



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
        screenWipe.Wipe(() =>
        {
            
            currentLives = 2;
            heartsUI.UpdateHearts(currentLives);

            Debug.Log("player dies");

            float oldDamping = confiner.Damping;
            float oldSlowingDistance = confiner.SlowingDistance;
            Vector3 oldPosition = player.transform.position;

            confiner.Damping = 0f;
            confiner.SlowingDistance = 0f;

            player.transform.position = currentCheckpoint;


            CinemachineCore.OnTargetObjectWarped(player.transform, currentCheckpoint - oldPosition);

            confiner.Damping = oldDamping;
            confiner.SlowingDistance = oldSlowingDistance;

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

            foreach (MovingPlatform platform in FindObjectsByType<MovingPlatform>())
            {
                platform.ResetPlatform();
            }

            foreach (Platform platform in FindObjectsByType<Platform>())
            {
                platform.Reset();
            }

        });
       
    }
}