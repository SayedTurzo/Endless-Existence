using System.Collections.Generic;
using EndlessExistence.Common;
using EndlessExistence.Third_Person_Control.Scripts;
using UnityEngine;
using UnityEngine.UI;


namespace EndlessExistence.Inventory.Scripts
{
    public class EE_Inventory : MonoBehaviour
    {
        private static EE_Inventory _instance;
        public static EE_Inventory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EE_Inventory>();
                }
                return _instance;
            }
        }
        
        public EE_ItemReferences itemReferences = new EE_ItemReferences();

        private GameObject _player;
        private ThirdPersonCharacterController _characterController;
        private Dictionary<string, int> _inventory;

        
        
        private Vector3 warningInitialPos;
        public List<GameObject> addedItems;
        
        public ItemDatabase itemDatabase;


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            _player = FindObjectOfType<ThirdPersonCharacterController>().gameObject;
            _characterController = _player.GetComponent<ThirdPersonCharacterController>();
            
            itemReferences.meleeButton.onClick.AddListener(() => FilterItem(EE_ItemDetail.ItemType.Melee));
            itemReferences.rangedButton.onClick.AddListener(() => FilterItem(EE_ItemDetail.ItemType.Ranged));
            itemReferences.defenceButton.onClick.AddListener(() => FilterItem(EE_ItemDetail.ItemType.Defence));
            itemReferences.potionButton.onClick.AddListener(() => FilterItem(EE_ItemDetail.ItemType.Potion));
            itemReferences.allButton.onClick.AddListener(FilterItem);
        }

        private void Start()
        {
            warningInitialPos = itemReferences.warningText.rectTransform.anchoredPosition;
            itemReferences.warningText.gameObject.SetActive(false);
            _inventory = new Dictionary<string, int>();
            itemReferences.inventoryCanvas.SetActive(false);
            
            // Load the ItemDatabase during startup
            LoadItemDatabase();

            foreach (EE_ItemDetail itemDetail in itemDatabase.items)
            {
                InitItemInDictionary(itemDetail);
            }
        }

        private void Update()
        {
            if (InputHandler.Instance.OpenInventoryTriggered)
            {
                _characterController.enabled = !_characterController.enabled;
                InputHandler.Instance.OpenInventoryTriggered = false;
                itemReferences.inventoryCanvas.SetActive(!itemReferences.inventoryCanvas.activeSelf);
                FilterItem();
                if (addedItems.Count==0)
                {
                    itemReferences.detailPanel.SetActive(false);
                }
                else
                {
                    SetDetailPanel(addedItems[0].GetComponent<EE_ItemDetailContainer>().itemDetail);
                }
            }
        }

        private void FilterItem(EE_ItemDetail.ItemType itemType)
        {
            itemReferences.indicatorText.text = itemType.ToString();
            if (itemType == EE_ItemDetail.ItemType.Melee)
            {
                itemReferences.indicator.GetComponent<RectTransform>().anchoredPosition = itemReferences.melee.anchoredPosition;
            }
            else if (itemType == EE_ItemDetail.ItemType.Ranged)
            {
                itemReferences.indicator.GetComponent<RectTransform>().anchoredPosition = itemReferences.ranged.anchoredPosition;
            }
            else if (itemType == EE_ItemDetail.ItemType.Defence)
            {
                itemReferences.indicator.GetComponent<RectTransform>().anchoredPosition = itemReferences.defence.anchoredPosition;
            }
            else if (itemType == EE_ItemDetail.ItemType.Potion)
            {
                itemReferences.indicator.GetComponent<RectTransform>().anchoredPosition = itemReferences.potion.anchoredPosition;
            }
            
            
            foreach (var item in addedItems)
            {
                item.SetActive(item.GetComponent<EE_ItemDetailContainer>().itemDetail.itemType == itemType);
            }
        }
        
        private void FilterItem()
        {
            itemReferences.indicatorText.text = "All";
            itemReferences.indicator.GetComponent<RectTransform>().anchoredPosition = itemReferences.all.anchoredPosition;
            foreach (var item in addedItems)
            {
                item.SetActive(true);
            }
        }
        
        
        public void AddItem(EE_ItemDetail item)
        {
            if (_inventory.ContainsKey(item.itemName) && item.stackable)
            {
                if (item.itemCurrentQuantity < item.maxStack)
                {
                    PickUpNotification(true,Color.green,"+ " + item.quantity + " " + item.itemName );
                    _inventory[item.itemName] += item.quantity;
                    item.itemCurrentQuantity++;
                    
// #if UNITY_EDITOR
//                     UnityEditor.EditorUtility.SetDirty(item);
//                     UnityEditor.AssetDatabase.SaveAssets();
//                     UnityEditor.AssetDatabase.Refresh();
// #endif
                }
                else
                {
                    PickUpNotification(true,Color.red,"Can't stack more than " + item.maxStack + " " + item.itemName+"!" );
                    Debug.Log("Maximum item reached");
                }
            }
            else if(item.itemCurrentQuantity < item.maxStack)
            {
                PickUpNotification(true,Color.green,"+ " + item.quantity + " " + item.itemName );
                // If the item doesn't exist, add it to the inventory
                _inventory.Add(item.itemName, item.quantity);
                item.itemCurrentQuantity++;
                
// #if UNITY_EDITOR
//                 UnityEditor.EditorUtility.SetDirty(item);
//                 UnityEditor.AssetDatabase.SaveAssets();
//                 UnityEditor.AssetDatabase.Refresh();
// #endif
                itemDatabase.items.Add(item);
                InstantiateItem(itemReferences.itemPrefab,item);
            }

            Debug.Log("Added " + item.quantity + " " + item.itemName + "(s) to the inventory.");
        }

        public void InitItemInDictionary(EE_ItemDetail item)
        {
            if (item != null)
            {
                // Your existing code here...
                if (_inventory.ContainsKey(item.itemName) && item.stackable)
                {
                    if (item.itemCurrentQuantity < item.maxStack)
                    {
                        _inventory[item.itemName] += item.quantity;  //item.itemCurrentQuantity;
                    }
                }
                else
                {
                    // If the item doesn't exist, add it to the inventory
                    _inventory.Add(item.itemName, item.quantity);   //_inventory.Add(item.itemName, item.itemCurrentQuantity);
                    InstantiateItem(itemReferences.itemPrefab,item);
                }

                Debug.Log("Added " + item.quantity + " " + item.itemName + "(s) to the inventory.");
            }
        }
        
        private void InstantiateItem(GameObject prefab,EE_ItemDetail itemDetail) //, string itemName ,string itemDes, Sprite icon , int amount ,int max
        {
            if (itemReferences.itemsHolder != null)
            {
                GameObject newItem = Instantiate(prefab, itemReferences.itemsHolder, false);
                addedItems.Add(newItem);
                newItem.GetComponent<EE_ItemDetailContainer>().itemDetail = itemDetail;
                newItem.gameObject.name = itemDetail.itemName;
                newItem.GetComponent<Image>().sprite = itemDetail.itemImage;
                newItem.transform.localPosition = Vector3.zero;
            }
            else
            {
                Debug.LogWarning("Canvas transform reference is missing. Cannot instantiate item.");
            }
        }

        public void SetDetailPanel(EE_ItemDetail itemDetail)
        {
            if (!itemReferences.detailPanel.activeSelf)
            {
                itemReferences.detailPanel.SetActive(true);
            }
            
            switch (itemDetail.itemType)
            {
                case EE_ItemDetail.ItemType.Melee:
                    SetActivePanel(itemReferences.meleeDetailPanel);
                    break;
                case EE_ItemDetail.ItemType.Ranged:
                    SetActivePanel(itemReferences.rangedDetailPanel);
                    break;
                case EE_ItemDetail.ItemType.Defence:
                    SetActivePanel(itemReferences.defenceDetailPanel);
                    break;
                case EE_ItemDetail.ItemType.Potion:
                    SetActivePanel(itemReferences.potionDetailPanel);
                    break;
            }
            
            itemReferences.itemName.text = itemDetail.itemName;
            itemReferences.itemQuantity.text = "Quantity : "+itemDetail.itemCurrentQuantity.ToString();
            itemReferences.itemDescription.text = itemDetail.itemDescription;
            itemReferences.itemImage.sprite = itemDetail.itemImage;
            itemReferences.maxStack.text = "Max stack : "+itemDetail.maxStack.ToString();
            
            itemReferences.dropButton.onClick.RemoveAllListeners();
            itemReferences.dropButton.onClick.AddListener(() => RemoveItem(itemDetail,itemDetail.itemName,1));
        }
        
        void SetActivePanel(GameObject activePanel)
        {
            itemReferences.meleeDetailPanel.SetActive(activePanel == itemReferences.meleeDetailPanel);
            itemReferences.rangedDetailPanel.SetActive(activePanel == itemReferences.rangedDetailPanel);
            itemReferences.defenceDetailPanel.SetActive(activePanel == itemReferences.defenceDetailPanel);
            itemReferences.potionDetailPanel.SetActive(activePanel == itemReferences.potionDetailPanel);
        }

        public void PickUpNotification(bool flag, Color color,string message)
        {
            itemReferences.warningText.gameObject.SetActive(flag);
            itemReferences.warningText.color = color;
            itemReferences.warningText.text = message;
            TextAnimation.MoveAndFadeText(itemReferences.warningText, warningInitialPos, new Vector3(0f, 450f, 0f), .5f, .5f);
        }

        // // Remove an item from the inventory
        // public void RemoveItem(string itemName, int quantity)
        // {
        //     if (_inventory.ContainsKey(itemName))
        //     {
        //         // If the item exists, decrease the quantity
        //         _inventory[itemName] -= quantity;
        //
        //         // If the quantity becomes zero or negative, remove the item from the inventory
        //         if (_inventory[itemName] <= 0)
        //         {
        //             _inventory.Remove(itemName);
        //         }
        //
        //         Debug.Log("Removed " + quantity + " " + itemName + "(s) from the inventory.");
        //     }
        //     else
        //     {
        //         Debug.Log("Item " + itemName + " not found in the inventory.");
        //     }
        // }
        
        // public void DisplayInventory()
        // {
        //     Debug.Log("Inventory Contents:");
        //     foreach (var item in _inventory)
        //     {
        //         Debug.Log(item.Key + ": " + item.Value);
        //     }
        // }


        private void OnApplicationQuit()
        {
// #if UNITY_EDITOR
//             UnityEditor.EditorUtility.SetDirty(itemDatabase);
//             UnityEditor.AssetDatabase.SaveAssets();
//             UnityEditor.AssetDatabase.Refresh();
// #endif
            SaveItemDatabase();
        }
        
        private void SaveItemDatabase()
        {
            string json = JsonUtility.ToJson(itemDatabase);
            PlayerPrefs.SetString("ItemDatabase", json);
        }
        
        private void LoadItemDatabase()
        {
            if (PlayerPrefs.HasKey("ItemDatabase"))
            {
                string json = PlayerPrefs.GetString("ItemDatabase");

                // Use ScriptableObject.CreateInstance to create an instance of ItemDatabase
                //itemDatabase = ScriptableObject.CreateInstance<ItemDatabase>();

                // Populate the fields of the created instance with the deserialized data
                JsonUtility.FromJsonOverwrite(json, itemDatabase);
            }
        }
        
        
        // Remove an item from the inventory
        public void RemoveItem(EE_ItemDetail itemDetail, string itemName, int quantity)
        {
            if (_inventory.ContainsKey(itemName))
            {
                // If the item exists, decrease the quantity
                _inventory[itemName] -= quantity;
                itemDetail.itemCurrentQuantity--;
                SetDetailPanel(itemDetail);

                // If the quantity becomes zero or negative, remove the item from the inventory
                if (itemDetail.itemCurrentQuantity <= 0)
                {
                    _inventory.Remove(itemName);

                    // Remove the item from the list
                    EE_ItemDetail itemToRemove = itemDatabase.items.Find(item => item.itemName == itemName);
                    if (itemToRemove != null)
                    {
                        itemDatabase.items.Remove(itemToRemove);
                    }

                    SaveItemDatabase();

                    // Remove the item from the list and destroy the associated GameObject
                    GameObject gameObjectToRemove = addedItems.Find(item =>
                        item.GetComponent<EE_ItemDetailContainer>().itemDetail.itemName == itemName);
                    if (gameObjectToRemove != null)
                    {
                        itemReferences.detailPanel.SetActive(false);
                        Debug.Log("Turzo: Closing detail panel "+itemReferences.detailPanel.activeSelf);
                        addedItems.Remove(gameObjectToRemove);
                        Destroy(gameObjectToRemove);
                    }

                    Debug.Log("Removed " + quantity + " " + itemName +
                              "(s) from the inventory, list, and destroyed associated GameObject.");
                }
                else
                {
                    Debug.Log("Decreased quantity of " + itemName + " to " + _inventory[itemName] + ".");
                }

                //SetDetailPanel(itemDetail); // Call this to update the UI
            }
            else
            {
                Debug.Log("Item " + itemName + " not found in the inventory.");
            }
        }
    }
}
