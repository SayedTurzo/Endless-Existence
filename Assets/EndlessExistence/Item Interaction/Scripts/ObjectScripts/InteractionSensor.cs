using System;
using System.Collections;
using EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts;
using EndlessExistence.Third_Person_Control.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public class InteractionSensor : MonoBehaviour
    {
        private GameObject _parent;
        private string _playerTag;
        public bool _canInteract;
       public bool isInspecting = false;

        private ObjectContainer _singleObjectScript;
        private EE_Object _baseObjectScript;
        private EE_InspectObject _inspectScript;

        private ThirdPersonCharacterController _playerControl;
        private Camera inspectorCamera;


        private Vector3 parentOriginalPos;
        private Quaternion parentOriginalRotation;

        private void Awake()
        {
            _parent = transform.parent.gameObject;
            _baseObjectScript = _parent.GetComponent<EE_Object>();
            _singleObjectScript = _parent.GetComponent<ObjectContainer>();
            if (_inspectScript!=null)
            {
                _parent.GetComponent<EE_InspectObject>();
            }
            _playerTag = _baseObjectScript.playerTag;
            inspectorCamera = FindObjectOfType<EE_InspectCamera>().inspectCamera;
            
        }

        private void Update()
        {
            if (_canInteract && InputHandler.Instance.InteractionTriggered && !_singleObjectScript.autoInteract)
            {
                _canInteract = false;
                ThingsToDoOnInteract();
            }
            
            if (_canInteract && InputHandler.Instance.InspectionTriggered && !_singleObjectScript.autoInteract)
            {
                _canInteract = false;
                _playerControl.enabled = false;
                DoInspection();
            }
            

            if (isInspecting && Input.GetKeyDown(KeyCode.Escape))
            {
                _playerControl.enabled = true;
                EndInspection();
            }
        }

        private void DoInspection()
        {
            //_playerControl.enabled = false;
            EE_InspectCamera inspectorCamScript = inspectorCamera.GetComponent<EE_InspectCamera>();
            inspectorCamScript.ToggleState(true);
            inspectorCamScript.ToggleLayer(_parent);
            inspectorCamScript.descriptionText.text =
                _parent.GetComponent<EE_InspectObject>().description;
            parentOriginalPos = _parent.transform.position;
            parentOriginalRotation = _parent.transform.rotation;

            if (_parent.GetComponent<Rigidbody>()!=null)
            {
                _parent.GetComponent<Rigidbody>().useGravity = false;
            }

            _parent.transform.position =
                inspectorCamera.GetComponent<EE_InspectCamera>().objectHolder.transform.position;
        
            if (_inspectScript==null)
            {
                _inspectScript = _parent.GetComponent<EE_InspectObject>();
            }
            _inspectScript.enabled = true;
            isInspecting = true;
        }

        private void EndInspection()
        {
            //_playerControl.enabled = true;
            EE_InspectCamera inspectorCamScript = inspectorCamera.GetComponent<EE_InspectCamera>();
            inspectorCamScript.ToggleState(false);
            inspectorCamScript.ToggleLayer(_parent);
            if (_parent.GetComponent<Rigidbody>()!=null)
            {
                _parent.GetComponent<Rigidbody>().useGravity = true;
            }
            _parent.transform.position = parentOriginalPos;
            _parent.transform.rotation = parentOriginalRotation;
                
            if (_inspectScript==null)
            {
                _inspectScript = _parent.GetComponent<EE_InspectObject>();
            }
            _inspectScript.enabled = false;
            isInspecting = false;
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
                _playerControl = other.gameObject.GetComponent<ThirdPersonCharacterController>();
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

                #region Door Special Conditions
                
                if (_singleObjectScript != null && _singleObjectScript.autoInteract)
                {
                    if (_singleObjectScript is EE_SimpleDoorObject or EE_SimpleDoubleDoor or EE_SlidingDoubleDoor or EE_SlidingSingleDoor)
                    {
                        ThingsToDoOnInteract();
                    }
                }

                #endregion
                
                _canInteract = false;
                _baseObjectScript.TriggerEffect();
            }
        }
    }
}
