using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_RangedWeapon : ObjectContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=red>Weapon Picked up</color>");
            AddObjectToInventory();
        }
    }
}
