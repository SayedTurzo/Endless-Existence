using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Slider defenseSlider;

    private IStatModifier statModifier;

    private void OnEnable()
    {
        statModifier = GetComponent<IStatModifier>();

        if (statModifier != null)
        {
            EventManager.Instance.OnHealthModified += UpdateHealthSlider;
            EventManager.Instance.OnStaminaModified += UpdateStaminaSlider;
            EventManager.Instance.OnDefenseModified += UpdateDefenseSlider;

            // Set initial values when the script is enabled
            SetInitialValuesToUI();
        }
        else
        {
            Debug.LogError("IStatModifier component is not implemented by the GameObject.");
        }
    }

    private void OnDisable()
    {
        if (statModifier != null)
        {
            EventManager.Instance.OnHealthModified -= UpdateHealthSlider;
            EventManager.Instance.OnStaminaModified -= UpdateStaminaSlider;
            EventManager.Instance.OnDefenseModified -= UpdateDefenseSlider;
        }
    }

    private void UpdateHealthSlider(int amount)
    {
        UpdateSlider(healthSlider, statModifier.CurrentHealth, statModifier.InitialHealth);
    }

    private void UpdateStaminaSlider(int amount)
    {
        UpdateSlider(staminaSlider, statModifier.CurrentStamina, statModifier.InitialStamina);
    }

    private void UpdateDefenseSlider(int amount)
    {
        UpdateSlider(defenseSlider, statModifier.CurrentDefense, statModifier.InitialDefense);
    }

    private void SetInitialValuesToUI()
    {
        UpdateSlider(healthSlider, statModifier.CurrentHealth, statModifier.InitialHealth);
        UpdateSlider(staminaSlider, statModifier.CurrentStamina, statModifier.InitialStamina);
        UpdateSlider(defenseSlider, statModifier.CurrentDefense, statModifier.InitialDefense);
    }

    private void UpdateSlider(Slider slider, int currentValue, int maxValue)
    {
        float ratio = (float)currentValue / maxValue;
        slider.value = ratio;
    }
}