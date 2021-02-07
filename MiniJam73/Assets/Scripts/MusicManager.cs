using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource _audioSource;
    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        _audioSource.clip = sounds[index];
        _audioSource.Play();
    }
}
