using System;
using __EndlessExistence._Third_Person_Control._Scripts;
using UnityEngine;

namespace __EndlessExistence.Item_Interaction.Scripts.ItemScripts
{
    public class InteractionSensor : MonoBehaviour
    {
        private EE_Item _parent;
        private string playerTag;
        private bool canInteract;

        private void Awake()
        {
            _parent = transform.parent.gameObject.GetComponent<EE_Item>();
            playerTag = _parent.playerTag;
        }

        private void Update()
        {
            if (canInteract && InputHandler.Instance.InteractionTriggered)
            {
                _parent.GetComponent<EE_IItem>().Interact();
                canInteract = false;
            }
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canInteract = true;
                _parent.TriggerCanvas();
                _parent.TriggerEffect();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canInteract = false;
                _parent.TriggerCanvas();
                _parent.TriggerEffect();
            }
        }
    }
}
