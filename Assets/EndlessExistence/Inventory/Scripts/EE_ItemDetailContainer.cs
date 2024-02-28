using System;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessExistence.Inventory.Scripts
{
    [Serializable]
    public class EE_ItemDetailContainer : MonoBehaviour
    {
        private Button button;
        public EE_ItemDetail itemDetail;

        private void Awake()
        {
            LoadItemDatabase();
        }

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => EE_Inventory.Instance.SetDetailPanel(itemDetail)); //itemDetail.itemName , itemDetail.itemDescription, itemDetail.itemImage , itemDetail.itemCurrentQuantity , itemDetail.maxStack)
        }

        private void OnApplicationQuit()
        {
            SaveItemDatabase();
        }

        private void SaveItemDatabase()
        {
            string json = JsonUtility.ToJson(itemDetail);
            PlayerPrefs.SetString(itemDetail.itemName, json);
        }
        
        private void LoadItemDatabase()
        {
            if (PlayerPrefs.HasKey(itemDetail.itemName))
            {
                string json = PlayerPrefs.GetString(itemDetail.itemName);

                // Use ScriptableObject.CreateInstance to create an instance of ItemDatabase
                //itemDetail = ScriptableObject.CreateInstance<EE_ItemDetail>();

                // Populate the fields of the created instance with the deserialized data
                JsonUtility.FromJsonOverwrite(json, itemDetail);
            }
        }
    }
}
