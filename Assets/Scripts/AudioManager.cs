using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioManager Instance;
    public float volume = 1f;
    public AudioSource gameSource;
    public AudioSource playerSource;

    public enum AudioType
    {
        Maillet,
        Scie
    }
    public enum AudioSourceType
    {
        Player, Game
    }
    [System.Serializable]
    public struct AudioData
    {
        public AudioClip clip;
        public AudioType type;
    }
    
    public AudioData[] data;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameSource.volume = volume;
        playerSource.volume = volume;   
    }

    public void playSound(AudioType type, AudioSourceType sourceType)
    {
        AudioClip clip = getClip(type);
        if (sourceType == AudioSourceType.Player)
        {
            playerSource.PlayOneShot(clip);
        }
        else if (sourceType == AudioSourceType.Game)
        {
            gameSource.PlayOneShot(clip);
        }

    }
    AudioClip getClip(AudioType type)
    {
        foreach (AudioData data in data)
        {
            if (data.type == type) { return data.clip; }
        }
        Debug.LogError("Pas troué le clip omg");
        return null;

    }
}
