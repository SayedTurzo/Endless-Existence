using System;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessExistence.Inventory.Scripts
{
    public class EE_ItemDetail : MonoBehaviour
    {
        public string itemName;
        public Sprite itemImage;
        public string itemDescription;
        public int itemCurrentQuantity;
        public int maxStack;

        private Button button;
        

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => EE_Inventory.Instance.SetDetailPanel(itemName , itemDescription, itemImage , itemCurrentQuantity , maxStack));
        }
    }
}
