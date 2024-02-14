using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ItemScripts.SingleItemScripts
{
    public class EE_HealthItem : ItemContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=green>Health used!</color>");
        }
    }
}
