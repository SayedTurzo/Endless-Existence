using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ItemScripts.SingleItemScripts
{
    public class EE_DefaultItem : ItemContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=green>Default item used!</color>");
        }
    }
}
