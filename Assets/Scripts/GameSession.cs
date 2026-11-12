using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int crystals = 0;
    public int totalCrystals = 3;

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }

    public GameState state = GameState.Playing;

    private void OnEnable()
    {
        GameEvents.CrystalCollected += AddCrystal;
        GameEvents.Won += OnWin;
        GameEvents.Lost += OnLose;
    }

    private void OnDisable()
    {
        GameEvents.CrystalCollected -= AddCrystal;
        GameEvents.Won -= OnWin;
        GameEvents.Lost -= OnLose;
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
        Debug.Log("Signal sent");
    }

    private void OnLose()
    {
        if (state != GameState.Playing)
            return;

        state = GameState.Lost;
        Debug.Log("Lost");
    }
    
    
}