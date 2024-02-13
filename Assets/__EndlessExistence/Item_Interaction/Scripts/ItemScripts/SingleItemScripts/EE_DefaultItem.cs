using CustomInspector;
using UnityEngine;

namespace __EndlessExistence.Item_Interaction.Scripts.ItemScripts.SingleItemScripts
{
    public class EE_DefaultItem : ItemContainer
    {
        public override void Interact()
        {
            Debug.Log("<color=green>Health used!</color>");
        }
    }
}
