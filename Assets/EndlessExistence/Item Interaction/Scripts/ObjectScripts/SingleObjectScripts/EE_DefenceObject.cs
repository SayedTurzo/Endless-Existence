using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_DefenceObject : ObjectContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=yellow>Defence used!</color>");
            AddObjectToInventory();
        }
    }
}
