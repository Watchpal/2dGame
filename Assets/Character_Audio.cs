using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip landClip;

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpClip);
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