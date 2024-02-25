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
        
        public ItemDatabase itemDatabase;


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
            if (_inventory.ContainsKey(item.itemDetail.itemName) && item.itemDetail.stackable)
            {
                if (_inventory[item.itemDetail.itemName] < item.itemDetail.maxStack)
                {
                    itemReferences.warningText.gameObject.SetActive(true);
                    itemReferences.warningText.color = Color.green;
                    itemReferences.warningText.text = "Added " + item.itemDetail.quantity + " " + item.itemDetail.itemName+"to inventory!";
                    TextAnimation.MoveAndFadeText(itemReferences.warningText, warningInitialPos, new Vector3(0f, 450f, 0f), .5f, .5f);
                    _inventory[item.itemDetail.itemName] += item.itemDetail.quantity;
                    SetItemQuantity(item.itemDetail.itemName,_inventory[item.itemDetail.itemName]);
                }
                else
                {
                    itemReferences.warningText.gameObject.SetActive(true);
                    itemReferences.warningText.color = Color.red;
                    itemReferences.warningText.text = "Can't stack more than " + item.itemDetail.maxStack + " " + item.itemDetail.itemName+"!";
                    TextAnimation.MoveAndFadeText(itemReferences.warningText,warningInitialPos, new Vector3(0f, 450f, 0f), .5f, .5f);
                    Debug.Log("Maximum item reached");
                }
            }
            else
            {
                itemReferences.warningText.gameObject.SetActive(true);
                itemReferences.warningText.color = Color.green;
                itemReferences.warningText.text = "Added " + item.itemDetail.quantity + " " + item.itemDetail.itemName+"to inventory!";
                TextAnimation.MoveAndFadeText(itemReferences.warningText,warningInitialPos, new Vector3(0f, 450f, 0f), .5f, .5f);
                // If the item doesn't exist, add it to the inventory
                _inventory.Add(item.itemDetail.itemName, item.itemDetail.quantity);
                InstantiateItem(itemReferences.itemPrefab,item.itemDetail, item.itemDetail.itemName ,item.itemDetail.itemDescription, item.itemDetail.itemImage , _inventory[item.itemDetail.itemName] , item.itemDetail.maxStack);
            }

            Debug.Log("Added " + item.itemDetail.quantity + " " + item.itemDetail.itemName + "(s) to the inventory.");
        }
        
        private void InstantiateItem(GameObject prefab,EE_ItemDetail itemDetail, string itemName ,string itemDes, Sprite icon , int amount ,int max)
        {
            if (itemReferences.itemsHolder != null)
            {
                GameObject newItem = Instantiate(prefab, itemReferences.itemsHolder, false);
                addedItems.Add(newItem);
                newItem.gameObject.name = itemName;
                newItem.GetComponent<Image>().sprite = icon;
                newItem.GetComponent<EE_ItemDetailContainer>().itemDetail = itemDetail;
                
                EE_ItemDetail newItemDetailSO = newItem.GetComponent<EE_ItemDetailContainer>().itemDetail;
                newItemDetailSO.itemName = itemName;
                newItemDetailSO.itemImage = icon;
                newItemDetailSO.itemDescription = itemDes;
                newItemDetailSO.itemCurrentQuantity = amount;
                newItemDetailSO.maxStack = max;
                //itemDetail.gameObject.GetComponent<Button>().onClick.AddListener(() => SetDetailPanel(itemName , itemDes, icon , amount));
                newItem.transform.localPosition = Vector3.zero;
                
                //itemDatabase.items.Add(itemDetail);
                // EE_ItemDetailContainer newItemDetail = ScriptableObject.CreateInstance<EE_ItemDetailContainer>().itemDetail;
                // newItemDetail.itemName = itemName;
                // newItemDetail.itemImage = icon;
                // newItemDetail.itemDescription = itemDes;
                // newItemDetail.itemCurrentQuantity = amount;
                // newItemDetail.maxStack = max;
                //itemDatabase.items.Add(newItemDetail);
                
                // Save changes to the AssetDatabase (optional, for persistent changes)
                UnityEditor.EditorUtility.SetDirty(itemDatabase);
                UnityEditor.AssetDatabase.SaveAssets();
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
