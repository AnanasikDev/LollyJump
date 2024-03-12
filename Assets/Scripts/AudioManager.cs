using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip, float volume = 1f)
    {
        audioSource.pitch = 1 + Random.Range(-0.075f, 0.075f);
        audioSource.PlayOneShot(clip, volume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
