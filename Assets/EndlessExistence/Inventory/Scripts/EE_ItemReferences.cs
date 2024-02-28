using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessExistence.Inventory.Scripts
{
    [Serializable]
    public class EE_ItemReferences
    {
        public GameObject inventoryCanvas;
        public GameObject itemPrefab;
        public Transform itemsHolder;
        public TextMeshProUGUI warningText;
        public GameObject detailPanel;

        [Header("Filtering Tabs")]
        public Button meleeButton;
        public Button rangedButton;
        public Button defenceButton;
        public Button potionButton;
        public Button allButton;
        
        
        // Details panel
        [Header("Item details panel")]
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemQuantity;
        public TextMeshProUGUI maxStack;
        public TextMeshProUGUI itemDescription;
        public Image itemImage;
        public Button dropButton;

        [Header("Specialized Item Type Details")]
        public GameObject meleeDetailPanel;
        public GameObject rangedDetailPanel;
        public GameObject defenceDetailPanel;
        public GameObject potionDetailPanel;

        [Header("Indicator Holders")] 
        public GameObject indicator;
        public TextMeshProUGUI indicatorText;
        public RectTransform all;
        public RectTransform melee;
        public RectTransform ranged;
        public RectTransform defence;
        public RectTransform potion;
    }
}
