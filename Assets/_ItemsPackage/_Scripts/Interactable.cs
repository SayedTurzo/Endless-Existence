using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum ItemType
    {
        Health,
        Stamina,
        Defence,
        Damage
    }
    
    public abstract void Interact();
    public abstract void Use(PlayerStatsModel playerStat);
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject interactCanvas;
    [SerializeField] protected TextMeshProUGUI interactText;
    [SerializeField] protected ItemType _itemType;
    [SerializeField] protected int modifyAmount;

    public void TriggerEffect()
    {
        effect.SetActive(!effect.activeSelf);
    }

    public void TriggerUI()
    {
        interactCanvas.SetActive(!interactCanvas.activeSelf);
    }
}
