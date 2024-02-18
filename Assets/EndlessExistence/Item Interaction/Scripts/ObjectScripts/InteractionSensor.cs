using System;
using EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts;
using EndlessExistence.Third_Person_Control.Scripts;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public class InteractionSensor : MonoBehaviour
    {
        private GameObject _parent;
        private string _playerTag;
        public bool _canInteract;

        private ObjectContainer _singleObjectScript;
        private EE_Object _baseObjectScript;

        private void Awake()
        {
            _parent = transform.parent.gameObject;
            _baseObjectScript = _parent.GetComponent<EE_Object>();
            _singleObjectScript = _parent.GetComponent<ObjectContainer>();
            _playerTag = _baseObjectScript.playerTag;
        }

        private void Update()
        {
            if (_canInteract && InputHandler.Instance.InteractionTriggered && !_singleObjectScript.autoInteract)
            {
                _canInteract = false;
                ThingsToDoOnInteract();
            }
        }

        private void ThingsToDoOnInteract()
        {
            if (_singleObjectScript!=null)
            {
                if (!_singleObjectScript.dontUseDefaultInteraction) _singleObjectScript.Interact(); 
                if (_singleObjectScript.destroyOnUse) _singleObjectScript.DestroyOnUse();
                if (_singleObjectScript.continuousInteraction && !_singleObjectScript.autoInteract) _singleObjectScript.SetCanInteractFlag(this);
                _singleObjectScript.onInteractWithItem?.Invoke();
            }
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                if (!_singleObjectScript.autoInteract)
                {
                    _baseObjectScript.TriggerCanvas();
                }
                else if (_singleObjectScript.autoInteract)
                {
                    ThingsToDoOnInteract();
                }
                
                _canInteract = true;
                _baseObjectScript.TriggerEffect();

            }
        }
        

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                if (!_singleObjectScript.autoInteract)
                {
                    _baseObjectScript.TriggerCanvas();
                }

                //Special condition for doors
                if (_parent.GetComponent<EE_SimpleDoorObject>()!=null && _singleObjectScript.autoInteract)
                {
                    ThingsToDoOnInteract();
                }
                
                _canInteract = false;
                _baseObjectScript.TriggerEffect();
            }
        }
    }
}
