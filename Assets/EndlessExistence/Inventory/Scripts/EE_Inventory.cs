using System;
using System.Collections.Generic;
using System.Linq;
using EndlessExistence.Common;
using EndlessExistence.Item_Interaction.Scripts.ObjectScripts;
using EndlessExistence.Third_Person_Control.Scripts;
using TMPro;
using UMGS;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace EndlessExistence.Inventory.Scripts
{
    public class EE_Inventory : SingletonPersistent<EE_Inventory>
    {
        [SerializeField] private GameObject inventoryCanvas;

        private GameObject _player;
        private ThirdPersonCharacterController _characterController;
        private Dictionary<string, int> _inventory;

        public EE_ItemReferences itemReferences = new EE_ItemReferences();
        
        private Vector3 warningInitialPos;
        public List<GameObject> addedItems;


        private void Awake()
        {
            _player = FindObjectOfType<ThirdPersonCharacterController>().gameObject;
            _characterController = _player.GetComponent<ThirdPersonCharacterController>();
        }

        private void Start()
        {
            if (addedItems.Count==0)
            {
                itemReferences.detailPanel.SetActive(false);
            }

            warningInitialPos = itemReferences.warningText.rectTransform.anchoredPosition;
            itemReferences.warningText.gameObject.SetActive(false);
            _inventory = new Dictionary<string, int>();
            inventoryCanvas.SetActive(false);
        }

        private void Update()
        {
            if (InputHandler.Instance.OpenInventoryTriggered)
            {
                _characterController.enabled = !_characterController.enabled;
                InputHandler.Instance.OpenInventoryTriggered = false;
                inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
                if (inventoryCanvas.activeSelf)
                {
                    EE_ItemDetail item = addedItems[0].GetComponent<EE_ItemDetail>();
                    SetDetailPanel(item.itemName , item.itemDescription, item.itemImage , item.itemCurrentQuantity ,item.maxStack);
                }
            }
        }
        
        
        public void AddItem(EE_ItemInventoryInfo item)
        {
            if (_inventory.ContainsKey(item.itemName) && item.stackable)
            {
                if (_inventory[item.itemName] < item.maxStack)
                {
                    itemReferences.warningText.gameObject.SetActive(true);
                    itemReferences.warningText.color = Color.green;
                    itemReferences.warningText.text = "Added " + item.quantity + " " + item.itemName+"to inventory!";
                    TextAnimation.MoveAndFadeText(itemReferences.warningText, warningInitialPos, new Vector3(0f, 450f, 0f), .5f, .5f);
                    _inventory[item.itemName] += item.quantity;
                    SetItemQuantity(item.itemName,_inventory[item.itemName]);
                }
                else
                {
                    itemReferences.warningText.gameObject.SetActive(true);
                    itemReferences.warningText.color = Color.red;
                    itemReferences.warningText.text = "Can't stack more than " + item.maxStack + " " + item.itemName+"!";
                    TextAnimation.MoveAndFadeText(itemReferences.warningText,warningInitialPos, new Vector3(0f, 450f, 0f), .5f, .5f);
                    Debug.Log("Maximum item reached");
                }
            }
            else
            {
                itemReferences.warningText.gameObject.SetActive(true);
                itemReferences.warningText.color = Color.green;
                itemReferences.warningText.text = "Added " + item.quantity + " " + item.itemName+"to inventory!";
                TextAnimation.MoveAndFadeText(itemReferences.warningText,warningInitialPos, new Vector3(0f, 450f, 0f), .5f, .5f);
                // If the item doesn't exist, add it to the inventory
                _inventory.Add(item.itemName, item.quantity);
                InstantiateItem(itemReferences.itemPrefab, item.itemName ,item.itemDescription, item.itemIcon , _inventory[item.itemName] , item.maxStack);
            }

            Debug.Log("Added " + item.quantity + " " + item.itemName + "(s) to the inventory.");
        }
        
        private void InstantiateItem(GameObject prefab, string itemName ,string itemDes, Sprite icon , int amount ,int max)
        {
            if (itemReferences.itemsHolder != null)
            {
                GameObject newItem = Instantiate(prefab, itemReferences.itemsHolder, false);
                addedItems.Add(newItem);
                newItem.gameObject.name = itemName;
                newItem.GetComponent<Image>().sprite = icon;
                EE_ItemDetail itemDetail = newItem.GetComponent<EE_ItemDetail>();
                itemDetail.itemName = itemName;
                itemDetail.itemImage = icon;
                itemDetail.itemDescription = itemDes;
                itemDetail.itemCurrentQuantity = amount;
                itemDetail.maxStack = max;
                //itemDetail.gameObject.GetComponent<Button>().onClick.AddListener(() => SetDetailPanel(itemName , itemDes, icon , amount));
                newItem.transform.localPosition = Vector3.zero;
            }
            else
            {
                Debug.LogWarning("Canvas transform reference is missing. Cannot instantiate item.");
            }
        }

        public void SetDetailPanel(string itemName,string itemDescription,Sprite icon , int quantity ,int maxStack)
        {
            if (!itemReferences.detailPanel.activeSelf)
            {
                itemReferences.detailPanel.SetActive(true);
            }
            itemReferences.itemName.text = itemName;
            itemReferences.itemQuantity.text = "Quantity : "+quantity.ToString();
            itemReferences.itemDescription.text = itemDescription;
            itemReferences.itemImage.sprite = icon;
            itemReferences.maxStack.text = "Max stack : "+maxStack.ToString();
        }

        private void SetItemQuantity(string itemName,int amount)
        {
            // Replace "YourObjectName" with the actual name you are looking for
            string targetObjectName = itemName;

            // Use LINQ to find the GameObject with the specified name
            GameObject targetObject = addedItems.FirstOrDefault(go => go.name == targetObjectName);

            if (targetObject != null)
            {
                // Do something with the found GameObject
                targetObject.GetComponent<EE_ItemDetail>().itemCurrentQuantity = amount;
            }
            else
            {
                // GameObject with the specified name was not found
                Debug.Log("GameObject with name " + targetObjectName + " not found");
            }
        }

        // Remove an item from the inventory
        public void RemoveItem(string itemName, int quantity)
        {
            if (_inventory.ContainsKey(itemName))
            {
                // If the item exists, decrease the quantity
                _inventory[itemName] -= quantity;

                // If the quantity becomes zero or negative, remove the item from the inventory
                if (_inventory[itemName] <= 0)
                {
                    _inventory.Remove(itemName);
                }

                Debug.Log("Removed " + quantity + " " + itemName + "(s) from the inventory.");
            }
            else
            {
                Debug.Log("Item " + itemName + " not found in the inventory.");
            }
        }
        
        public void DisplayInventory()
        {
            Debug.Log("Inventory Contents:");
            foreach (var item in _inventory)
            {
                Debug.Log(item.Key + ": " + item.Value);
            }
        }
    }
}
