using UnityEngine;

public class ObjectiveDestroyer : MonoBehaviour
    {
   public PlayerLives playerLives; // >Drag Player here in Inspector
public GameObject objective;

void Update()
{
    //if (PlayerLives.Lives == 1)
    {
        Destroy(objective);
    }
}
 }
