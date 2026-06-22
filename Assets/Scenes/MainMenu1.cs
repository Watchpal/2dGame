using UnityEngine;

public class MainMenu1 : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackToMain()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
