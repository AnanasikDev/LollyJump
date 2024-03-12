using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audioSource;
    private void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip, float volume = 1f)
    {
        audioSource.pitch = 1 + Random.Range(-0.125f, 0.125f);
        audioSource.PlayOneShot(clip, volume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
