using System;

public class EventManager
{
    public static EventManager Instance { get; private set; } = new EventManager();

    public event Action<int> OnHealthModified;
    public event Action<int> OnStaminaModified;
    public event Action<int> OnDefenseModified;

    public event Action OnHealthReset;
    public event Action OnStaminaReset;
    public event Action OnDefenceReset;
    public void NotifyOnHealthModified(int amount)
    {
        OnHealthModified?.Invoke(amount);
    }

    public void NotifyOnStaminaModified(int amount)
    {
        OnStaminaModified?.Invoke(amount);
    }

    public void NotifyOnDefenseModified(int amount)
    {
        OnDefenseModified?.Invoke(amount);
    }
    
    public void NotifyOnHealthReset()
    {
        OnHealthReset?.Invoke();
    }
    
    public void NotifyOnStaminaReset()
    {
        OnStaminaReset?.Invoke();
    }
    
    public void NotifyOnDefenceReset()
    {
        OnDefenceReset?.Invoke();
    }
}