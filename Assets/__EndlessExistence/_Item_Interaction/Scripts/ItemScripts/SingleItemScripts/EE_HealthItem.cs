using UnityEngine;

namespace __EndlessExistence._Item_Interaction.Scripts.ItemScripts.SingleItemScripts
{
    public class EE_HealthItem : ItemContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=green>Health used!</color>");
        }
    }
}
