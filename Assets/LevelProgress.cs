using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    public Transform player;
    public float levelStartX;
    public float levelEndX;
    public Slider progressBar;

    private float maxProgress = 0f;

    void Update()
    {
        float currentProgress = Mathf.InverseLerp(levelStartX, levelEndX, player.position.x);

        if (currentProgress > maxProgress)
        {
            maxProgress = currentProgress;
        }
        progressBar.value = maxProgress;
    }
}
