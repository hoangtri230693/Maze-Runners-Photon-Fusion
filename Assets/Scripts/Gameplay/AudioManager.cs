using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource SoundBGM;
    public AudioClip BackgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundBGM.loop = true;
        SoundBGM.clip = BackgroundMusic;
        SoundBGM.Play();
    }
}
