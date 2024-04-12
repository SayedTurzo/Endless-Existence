using System;
using UnityEngine;

namespace EndlessExistence.Inventory.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewEE_ItemDetail", menuName = "Inventory Item/EE_ItemDetail", order = 1)]
    public class EE_ItemDetail : ScriptableObject
    {
        public enum ItemType
        {
            Melee,
            Ranged,
            Defence,
            Potion
        }

        public ItemType itemType;
        public string itemName;
        public Sprite itemImage;
        public string itemDescription;
        public int quantity;
        public int itemCurrentQuantity;
        public bool stackable = true;
        public int maxStack=1;
    }
}