using UnityEngine;

/// <summary>
/// Singleton class to store all sound clips in the game
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundBase : MonoBehaviour
{
    private static SoundBase _instance;
    
    public AudioClip click;
    public AudioClip[] combo;
    public AudioClip[] swish;
    public AudioClip bug;
    public AudioClip bugDissapier;
    public AudioClip pops;
    public AudioClip hit;
    public AudioClip kreakWheel;
    public AudioClip spark;
    public AudioClip winSound;
    public AudioClip gameOver;
    public AudioClip alert;
    public AudioClip aplauds;
    public AudioClip OutOfMoves;
    public AudioClip black_hole;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _instance = this;
    }

    public static SoundBase GetInstance()
    {
        return _instance;
    }
}
