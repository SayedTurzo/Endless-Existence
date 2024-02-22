using EndlessExistence.Inventory.Scripts;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_StaminaObject : ObjectContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=blue>Stamina used!</color>");
            AddObjectToInventory();
        }
    }
}
