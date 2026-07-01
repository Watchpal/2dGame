using System;
using System.Collections;
using UnityEngine;

public class ScreenWipe : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private float duration = 0.9f;

    private float panelHeight;

    private void Awake()
    {
        if (panel == null)
            panel = GetComponent<RectTransform>();

        panelHeight = panel.rect.height;

        // Start hidden above the screen
        panel.anchoredPosition = new Vector2(0f, panelHeight);
    }

    public void Wipe(Action action)
    {
        StartCoroutine(WipeRoutine(action));
    }

    private IEnumerator WipeRoutine(Action action)
    {
        CharacterMovement player = FindAnyObjectByType<CharacterMovement>();

        if (player != null)
            player.SetMovementEnabled(false);

        yield return WipeIn();

        // Execute whatever the GameManager wants while the screen is black.
        action?.Invoke();

        yield return WipeOut(player);

        if (player != null)
            player.SetMovementEnabled(true);
    }

    private IEnumerator WipeIn()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float progress = Mathf.Clamp01(t / duration);
            progress = 1f - Mathf.Pow(1f - progress, 3f); // Ease-out cubic

            float y = Mathf.Lerp(panelHeight, 0f, progress);
            panel.anchoredPosition = new Vector2(0f, y);

            yield return null;
        }

        panel.anchoredPosition = Vector2.zero;
    }

    private IEnumerator WipeOut(CharacterMovement player)
    {
        float t = 0f;
        bool movementEnabled = false;

        while (t < duration)
        {
            t += Time.deltaTime;

            float progress = Mathf.Clamp01(t / duration);
            progress = 1f - Mathf.Pow(1f - progress, 3f); // Ease-out cubic

            float y = Mathf.Lerp(0f, -panelHeight, progress);
            panel.anchoredPosition = new Vector2(0f, y);

            if (!movementEnabled && progress >= 0.4f)
            {
                movementEnabled = true;

                if (player != null)
                    player.SetMovementEnabled(true);
            }

            yield return null;
        }

        panel.anchoredPosition = new Vector2(0f, -panelHeight);

        if (!movementEnabled && player != null)
            player.SetMovementEnabled(true);
    }
}