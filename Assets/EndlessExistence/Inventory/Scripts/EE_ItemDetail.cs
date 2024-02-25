using System;
using CustomInspector;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessExistence.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "NewEE_ItemDetail", menuName = "Inventory Item/EE_ItemDetail", order = 1)]
    public class EE_ItemDetail : ScriptableObject
    {
        public string itemName;
        public Sprite itemImage;
        public string itemDescription;
        public int quantity;
        public int itemCurrentQuantity;
        public bool stackable = true;
        [ShowIf(nameof(stackable))] public int maxStack=3;
    }
}