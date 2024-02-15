using System;
using System.Collections;
using CustomInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public abstract class ObjectContainer : MonoBehaviour,EE_IObject
    {
        [MessageBox("Description = Item Description \n Strength = How much effect the item will have.", MessageBoxType.Info)]
        [SerializeField] private bool haveDescription = false;
        [ShowIf(nameof(haveDescription))][SerializeField] private string description;
        [SerializeField] private float strength;

        [HorizontalLine("Interaction Settings")]
        public bool autoInteract = false;
        public bool continuousInteraction;
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
                gameObject.GetComponent<EE_Object>().objectDescriptionPanel.SetActive(false);
            }

        }

        public void SetDescription()
        {
            gameObject.GetComponent<EE_Object>().itemDescription.text = Description;
        }

        public abstract void Interact();

        public void SetCanInteractFlag(InteractionSensor sensor)
        {
            StartCoroutine(Delay(sensor));
        }
        
        IEnumerator Delay(InteractionSensor sensor)
        {
            yield return new WaitForSeconds(.2f);
            sensor._canInteract = true;
        }

        public void DestroyOnUse()
        {
            Destroy(this.gameObject);
        }
    }
}
