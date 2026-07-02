using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    

    [SerializeField] private AudioClip[] jumpClips;

    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip landClip;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayRandom(AudioClip[] clips, float minPitch = 0.95f, float maxPitch = 1.05f)
    {
        if (clips == null || clips.Length == 0)
            return;

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    public void PlayJump()
    {
        PlayRandom(jumpClips);
    }

    public void PlayHurt()
    {
        audioSource.PlayOneShot(hurtClip);
    }

    public void PlayWalk()
    {
        audioSource.PlayOneShot(walkClip);
    }

    public void PlayLand()
    {
        audioSource.PlayOneShot(landClip);
    }
}