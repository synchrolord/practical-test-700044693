using System;

public static class GameEvents
{
    public static event Action CrystalCollected;
    public static event Action Won;
    public static event Action Lost;
    public static event Action Burned;


    public static event Action BeaconActivated;

    public static void CollectCrystal()
    {
        CrystalCollected?.Invoke();
    }

    public static void Win()
    {
        Won?.Invoke();
    }

    public static void Lose()
    {
        Lost?.Invoke();
    }
    
    
    
    public static void ActivateBeacon()
    {
        BeaconActivated?.Invoke();
    }
    
    

    public static void PlayerBurned()
    {
        Burned?.Invoke();
    }
}