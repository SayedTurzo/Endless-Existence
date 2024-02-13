using UnityEditor;

public class CollectableItem : Interactable
{
    public override void Interact()
    {
        switch (_itemType)
        {
            case ItemType.Health:
                interactText.text = "Health";
                break;

            case ItemType.Stamina:
                interactText.text = "Stamina";
                break;

            case ItemType.Defence:
                interactText.text = "Defence";
                break;
            
            case ItemType.Damage:
                interactText.text = "Damage";
                break;

            default:
                interactText.text = "Unknown Type";
                break;
        }
    }

    public override void Use(PlayerStatsModel playerStat)
    {
        switch (_itemType)
        {
            case ItemType.Health:
                playerStat.ModifyHealth(modifyAmount);
                break;

            case ItemType.Stamina:
                playerStat.ModifyStamina(modifyAmount);
                break;

            case ItemType.Defence:
                playerStat.ModifyDefense(modifyAmount);
                break;
            
            case ItemType.Damage:
                playerStat.ModifyHealth(modifyAmount);
                break;
        }
    }
}
