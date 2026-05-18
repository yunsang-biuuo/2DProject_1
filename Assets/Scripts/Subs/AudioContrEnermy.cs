using UnityEngine;

public class AudioContrEnermy : MonoBehaviour
{
    private AudioSource _audioSource;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
    }
    public void PlayRoam()
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }
    public void StopRoam()
    {
        _audioSource.Stop();
    }
}