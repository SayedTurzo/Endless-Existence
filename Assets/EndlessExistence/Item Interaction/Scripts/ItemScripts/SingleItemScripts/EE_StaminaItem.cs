using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ItemScripts.SingleItemScripts
{
    public class EE_StaminaItem : ItemContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=blue>Stamina used!</color>");
        }
    }
}
