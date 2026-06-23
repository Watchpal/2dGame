using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        float saved = SettingsManager.Instance.LoadVolume();
        volumeSlider.value = saved;
        SettingsManager.Instance.ApplyVolume(saved);
    }

    public void OnVolumeChanged(float value)
    {
        SettingsManager.Instance.ApplyVolume(value);
        SettingsManager.Instance.SaveVolume(value);
    }
}