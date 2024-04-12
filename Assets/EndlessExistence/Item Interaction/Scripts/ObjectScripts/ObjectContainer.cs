using EndlessExistence.Inventory.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public abstract class ObjectContainer : MonoBehaviour,EE_IObject
    {
        [Tooltip("Description = Item Description \n Strength = How much effect the item will have.")]
        [SerializeField] private bool haveDescription = false;
        private string description;
        [SerializeField] private float strength;

        
        public bool autoInteract = false;
        public bool continuousInteraction;
        public bool dontUseDefaultInteraction = false;
        public bool destroyOnUse = false;
        [Header("Custom Event")]
        public UnityEvent onInteractWithItem;

        internal EE_ItemDetail _itemDetail;
        
        EE_ItemInventoryInfo itemInventoryInfo;
    
        public string Description
        {
            get => description;
            set => description = value;
        }

        public float Value
        {
            get => strength ;
            set => strength = value;
        }
        
        public bool HaveDescription
        {
            get => haveDescription ;
            set => haveDescription = value;
        }

        private void OnEnable()
        {
            if (haveDescription)
            {
                SetDescription();
            }
            else
            {
                gameObject.GetComponent<EE_Object>().objectDescriptionPanel.SetActive(false);
            }

        }

        private void Start()
        {
            if (gameObject.TryGetComponent(out itemInventoryInfo))
            {
                _itemDetail = itemInventoryInfo.itemDetail;
            }
        }

        public void SetDescription()
        {
            gameObject.GetComponent<EE_Object>().objectDescription.text = Description;
        }

        protected void AddObjectToInventory()
        {
            if (_itemDetail!=null)
            {
                EE_Inventory.Instance.AddItem(_itemDetail);
            }
        }

        public abstract void Interact();
        
        public void DestroyOnUse()
        {
            Destroy(this.gameObject);
        }
    }
}
