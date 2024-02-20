using EndlessExistence.Third_Person_Control.Scripts;
using UnityEngine;

namespace EndlessExistence.Inventory.Scripts
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
                InputHandler.Instance.OpenInventoryTriggered = false;
                inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            }
        }
    }
}
