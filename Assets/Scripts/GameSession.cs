using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int crystals = 0;
    public int totalCrystals = 3;

    public AudioClip musicSound;
    public AudioClip winSound;
    public AudioClip failSound;
    private AudioSource audioSource;
    
    
    public int burns = 0;
    public int maxBurns = 3;

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }

    public GameState state = GameState.Playing;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.volume = 0.25f;
        audioSource.clip = musicSound;

        if (musicSound != null)
            audioSource.Play();
    }


    private void OnEnable()
    {
        GameEvents.CrystalCollected += AddCrystal;
        GameEvents.Won += OnWin;
        GameEvents.Lost += OnLose;
        
        GameEvents.Burned += OnBurned;
    }

    private void OnDisable()
    {
        GameEvents.CrystalCollected -= AddCrystal;
        GameEvents.Won -= OnWin;
        GameEvents.Lost -= OnLose;
        
        GameEvents.Burned -= OnBurned;
    }


    private void AddCrystal()
    {
        if (state != GameState.Playing)
            return;

        crystals++;

        Debug.Log("Crystals " + crystals + " / " + totalCrystals);

        if (crystals >= totalCrystals)
        {
            GameEvents.ActivateBeacon();
        }
    }


    private void OnWin()
    {
        if (state != GameState.Playing)
            return;

        state = GameState.Won;
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.volume = 1f;
        
        if (winSound != null) 
            audioSource.PlayOneShot(winSound);
        
        
        Debug.Log("Signal sent");
    }

    private void OnLose()
    {
        if (state != GameState.Playing)
            return;

        state = GameState.Lost;
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.volume = 1f;
        if (failSound != null)
            audioSource.PlayOneShot(failSound);
        Debug.Log("Lost");
    }
    
    
    private void OnBurned()
    {
        if (state != GameState.Playing)
            return;

        burns++;

        Debug.Log("Burns " + burns + " / " + maxBurns);

        if (burns >= maxBurns)
        {
            GameEvents.Lose();
        }
    }

}