using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_HealthObject : ObjectContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=green>Health used!</color>");
            AddObjectToInventory();
        }
    }
}
