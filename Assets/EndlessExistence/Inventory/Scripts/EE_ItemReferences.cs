using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessExistence.Inventory.Scripts
{
    [Serializable]
    public class EE_ItemReferences
    {
        public GameObject itemPrefab;
        public Transform itemsHolder;
        public TextMeshProUGUI warningText;
        public GameObject detailPanel;
        
        // Details panel
        [Header("Item details panel")]
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemQuantity;
        public TextMeshProUGUI maxStack;
        public TextMeshProUGUI itemDescription;
        public Image itemImage;
    }
}
