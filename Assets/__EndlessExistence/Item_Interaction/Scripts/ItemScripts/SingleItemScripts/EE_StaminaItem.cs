using CustomInspector;
using UnityEngine;

namespace __EndlessExistence.Item_Interaction.Scripts.ItemScripts.SingleItemScripts
{
    public class EE_StaminaItem : ItemContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=blue>Stamina used!</color>");
        }
    }
}
