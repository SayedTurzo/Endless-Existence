using CustomInspector;
using UnityEngine;

namespace __EndlessExistence.Item_Interaction.Scripts.ItemScripts.SingleItemScripts
{
    public class EE_DefenceItem : MonoBehaviour , EE_IItem
    {
        [MessageBox("Description = Item Description \n Value = How much effect the item will have.", MessageBoxType.Info)]
        [SerializeField] private bool haveDescription = true;
        [ShowIf(nameof(haveDescription))][SerializeField] private string description;
        [SerializeField] private float effectValue;
    
        public string Description
        {
            get => description;
            set => description = value;
        }

        public float Value
        {
            get => effectValue ;
            set => effectValue = value;
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

        public void Interact()
        {
            Debug.Log("<color=yellow>Defence used!</color>");
        }
    }
}
