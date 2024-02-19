using System;
using System.Collections;
using EndlessExistence.Third_Person_Control.Scripts;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public class EE_InspectObject : MonoBehaviour
    {
        [SerializeField] private float speed=.5f;
        private Vector2 rotation;
        private bool rotationAllowed;
        public string description;
        

        private void Start()
        {
            gameObject.GetComponent<EE_Object>().objectInspectPanel.SetActive(true);
            gameObject.GetComponent<EE_InspectObject>().enabled = false;
        }
        

        private void Update()
        {
            rotation = InputHandler.Instance.LookInput;
            rotationAllowed = InputHandler.Instance.SelectionTriggered;
            if (rotationAllowed)
            {
                StartCoroutine(RotateObject());
            }
        }

        private IEnumerator RotateObject()
        {
            Camera inspectCamera = EE_InspectCamera.Instance.inspectCamera;
            rotation *= speed;
            transform.Rotate(Vector3.up,rotation.x,Space.World);
            transform.Rotate(-inspectCamera.transform.right,rotation.y,Space.World);
            yield return null;
        }
    }
}
