using System.Collections.Generic;
using UnityEngine;

namespace EndlessExistence.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database", order = 1)]
    public class ItemDatabase : ScriptableObject
    {
        public List<EE_ItemDetailContainer> items = new List<EE_ItemDetailContainer>();
    }
}