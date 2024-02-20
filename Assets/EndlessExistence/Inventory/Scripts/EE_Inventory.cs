using System;
using EndlessExistence.Third_Person_Control.Scripts;
using UnityEngine;

namespace EndlessExistence.Inventory
{
    public class EE_Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryCanvas;

        private bool inventoryIsopen = false;

        private void Start()
        {
            inventoryCanvas.SetActive(false);
        }

        private void Update()
        {
            if (InputHandler.Instance.OpenInventoryTriggered)
            {
                Debug.Log("triggered open");
    
                inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            }
        }
    }
}
