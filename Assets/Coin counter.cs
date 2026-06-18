using UnityEngine;
using TMPro;
public class Coincounter : MonoBehaviour
{
    public static Coincounter instance;

    private void Awake()
    {
        instance = this;
    }


    public int coins = 0;
    public TextMeshProUGUI coinText;
    public void AddCoin (int amount)
    {
        coins += amount;
        coinText.text = ":" + coins;
    }
}
