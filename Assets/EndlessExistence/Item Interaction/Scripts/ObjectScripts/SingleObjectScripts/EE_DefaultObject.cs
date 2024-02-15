using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_DefaultObject : ObjectContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=green>Default item used!</color>");
        }
    }
}
