using System;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessExistence.Inventory.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database", order = 1)]
    public class ItemDatabase : ScriptableObject
    {
        public List<EE_ItemDetail> items = new List<EE_ItemDetail>();
    }
}
