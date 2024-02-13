using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsModel playerStats;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Slider defenseSlider;

    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStatsModel not assigned in the inspector.");
            return;
        }

        InitializeStats();
        SubscribeToStatEvents();
    }

    private void InitializeStats()
    {
        // Set current values to max values on game start
        playerStats.CurrentHealth = playerStats.MaxHealth;
        playerStats.CurrentStamina = playerStats.MaxStamina;
        playerStats.CurrentDefense = playerStats.MaxDefense;

        // Set sliders
        if (healthSlider != null)
            healthSlider.value = (float)playerStats.CurrentHealth / playerStats.MaxHealth;

        if (staminaSlider != null)
            staminaSlider.value = (float)playerStats.CurrentStamina / playerStats.MaxStamina;

        if (defenseSlider != null)
            defenseSlider.value = (float)playerStats.CurrentDefense / playerStats.MaxDefense;
    }

    private void SubscribeToStatEvents()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthModified += UpdateHealthSlider;
            playerStats.OnStaminaModified += UpdateStaminaSlider;
            playerStats.OnDefenseModified += UpdateDefenseSlider;
        }
    }

    private void UpdateHealthSlider(int current, int max)
    {
        UpdateSlider(healthSlider, current, max);
    }

    private void UpdateStaminaSlider(int current, int max)
    {
        UpdateSlider(staminaSlider, current, max);
    }

    private void UpdateDefenseSlider(int current, int max)
    {
        UpdateSlider(defenseSlider, current, max);
    }

    private void UpdateSlider(Slider slider, int currentValue, int maxValue)
    {
        float ratio = (float)currentValue / maxValue;
        slider.value = ratio;
    }

    private void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthModified -= UpdateHealthSlider;
            playerStats.OnStaminaModified -= UpdateStaminaSlider;
            playerStats.OnDefenseModified -= UpdateDefenseSlider;
        }
    }
}
