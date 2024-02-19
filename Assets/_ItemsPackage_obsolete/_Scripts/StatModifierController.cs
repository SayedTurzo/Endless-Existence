using UnityEngine;

public class StatModifierController : MonoBehaviour
{
    [SerializeField] private PlayerStatsModel playerStatsModel;

    private void Start()
    {
        if (playerStatsModel == null)
        {
            Debug.LogError("PlayerStatsModel is not assigned in the Inspector.");
            return;
        }

        // Example: Modify health by 20 when the game starts
        // playerStatsModel.ModifyHealth(20);
    }

    private void ModifyHealth(int amount)
    {
        EventManager.Instance.NotifyOnHealthModified(amount);
    }

    private void ModifyStamina(int amount)
    {
        EventManager.Instance.NotifyOnStaminaModified(amount);
    }

    private void ModifyDefense(int amount)
    {
        EventManager.Instance.NotifyOnDefenseModified(amount);
    }
}