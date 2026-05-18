using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.Play();
    }
}