using __EndlessExistence._Third_Person_Control._Scripts;
using UnityEngine;

namespace __EndlessExistence._Item_Interaction.Scripts.ItemScripts
{
    public class InteractionSensor : MonoBehaviour
    {
        private GameObject _parent;
        private string _playerTag;
        private bool _canInteract;

        private ItemContainer _singleItemScript;
        private EE_Item _baseItemScript;

        private void Awake()
        {
            _parent = transform.parent.gameObject;
            _baseItemScript = _parent.GetComponent<EE_Item>();
            _singleItemScript = _parent.GetComponent<ItemContainer>();
            _playerTag = _baseItemScript.playerTag;
        }

        private void Update()
        {
            if (_canInteract && InputHandler.Instance.InteractionTriggered)
            {
                ThingsToDoOnInteract();
                _canInteract = false;
            }
        }

        private void ThingsToDoOnInteract()
        {
            _singleItemScript.Interact();
            if (_singleItemScript.destroyOnUse)
            {
                _singleItemScript.DestroyOnUse();
            }
            _singleItemScript.onInteractWithItem?.Invoke();
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _canInteract = true;
                _baseItemScript.TriggerCanvas();
                _baseItemScript.TriggerEffect();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _canInteract = false;
                _baseItemScript.TriggerCanvas();
                _baseItemScript.TriggerEffect();
            }
        }
    }
}
