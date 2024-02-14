using CustomInspector;
using UnityEngine;
using UnityEngine.Events;

namespace __EndlessExistence._Item_Interaction.Scripts.ItemScripts
{
    public abstract class ItemContainer : MonoBehaviour,EE_IItem
    {
        [MessageBox("Description = Item Description \n Strength = How much effect the item will have.", MessageBoxType.Info)]
        [SerializeField] private bool haveDescription = true;
        [ShowIf(nameof(haveDescription))][SerializeField] private string description;
        [SerializeField] private float strength;

        [HorizontalLine("Interaction Settings")]
        public bool dontUseDefaultInteraction = false;
        public bool destroyOnUse = false;
        [Header("Custom Event")]
        public UnityEvent onInteractWithItem;
        
    
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
                gameObject.GetComponent<EE_Item>().itemDescriptionPanel.SetActive(false);
            }

        }

        public void SetDescription()
        {
            gameObject.GetComponent<EE_Item>().itemDescription.text = Description;
        }

        public abstract void Interact();

        public void DestroyOnUse()
        {
            Destroy(this.gameObject);
        }
    }
}
