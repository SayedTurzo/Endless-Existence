using UnityEngine;

[System.Serializable]
public class PlayerStatsModel : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxStamina;
    [SerializeField] private int maxDefense;

    private int currentHealth;
    private int currentStamina;
    private int currentDefense;

    public int MaxHealth => maxHealth;
    public int MaxStamina => maxStamina;
    public int MaxDefense => maxDefense;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, MaxHealth); }
    }

    public int CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = Mathf.Clamp(value, 0, MaxStamina); }
    }

    public int CurrentDefense
    {
        get { return currentDefense; }
        set { currentDefense = Mathf.Clamp(value, 0, MaxDefense); }
    }

    public delegate void StatModifiedEvent(int current, int max);
    public event StatModifiedEvent OnHealthModified;
    public event StatModifiedEvent OnStaminaModified;
    public event StatModifiedEvent OnDefenseModified;

    private void NotifyHealthModified()
    {
        OnHealthModified?.Invoke(CurrentHealth, MaxHealth);
    }

    private void NotifyStaminaModified()
    {
        OnStaminaModified?.Invoke(CurrentStamina, MaxStamina);
    }

    private void NotifyDefenseModified()
    {
        OnDefenseModified?.Invoke(CurrentDefense, MaxDefense);
    }

    public void ModifyHealth(int amount)
    {
        CurrentHealth += amount;
        NotifyHealthModified();
    }

    public void ModifyStamina(int amount)
    {
        CurrentStamina += amount;
        NotifyStaminaModified();
    }

    public void ModifyDefense(int amount)
    {
        CurrentDefense += amount;
        NotifyDefenseModified();
    }
}