using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public Image[] hearts;

    public void UpdateHearts(int lives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < lives;
        }
    }
}