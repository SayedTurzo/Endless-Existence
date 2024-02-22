using UnityEngine;
using CustomInspector;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public class EE_ItemInventoryInfo : MonoBehaviour
    {
        public string itemName;
        public string itemDescription;
        public int quantity;
        public Sprite itemIcon;
        public bool stackable = true;
        [ShowIf(nameof(stackable))] public int maxStack=3;
    }
}
