using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [Header("Audio")]
    public AudioMixer audioMixer;

    const string MASTER_VOLUME_KEY = "volume";

    public float volume;

    void Awake()
    { 
        // Simple singleton so other scripts can access it easily
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
           Destroy(gameObject);
           return;
        }
    }

        public float LoadVolume()
    
        {
           PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        return volume;
        }

        public void SaveVolume(float value)
        {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, value);
        }

        public void ApplyVolume(float value)
        {
           float dB = Mathf.Log10(Mathf.Clamp(value, 0, 0001f)) * 20f;
           audioMixer.SetFloat("MasterVolume", dB);
        }
    
}


