using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class CameraZoomOut : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;

    public float normalFOV = 60f;
    public float zoomOutFOV = 80f;
    public float zoomDuration = 1f;

    Coroutine zoomRoutine;

    public void ZoomOut()
    {
        if (zoomRoutine != null)
            StopCoroutine(zoomRoutine);

        zoomRoutine = StartCoroutine(ZoomTo(zoomOutFOV));
    }

    public void ZoomIn()
    {
        if (zoomRoutine != null)
            StopCoroutine(zoomRoutine);

        zoomRoutine = StartCoroutine(ZoomTo(normalFOV));
    }

    IEnumerator ZoomTo(float targetFOV)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float time = 0f;

        while (time < zoomDuration)
        {
            time += Time.deltaTime;

            cinemachineCamera.Lens.FieldOfView =
                Mathf.Lerp(startFOV, targetFOV, time / zoomDuration);

            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}