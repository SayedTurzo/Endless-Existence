using TMPro;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts
{
    public class EE_InspectCamera : MonoBehaviour
    {
        public static EE_InspectCamera Instance;

        public Camera inspectCamera;

        public GameObject objectHolder;
        public TextMeshProUGUI descriptionText;

        public GameObject effect;
        public GameObject inspectCanvas;

    
        public int targetLayerIndex;
        private string _targetLayerName = "InspectableItem";
                                                                                                                                                    
        private int _layerIndex;
        private string _defaultLayerName;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            _targetLayerName = LayerMask.LayerToName(targetLayerIndex);
            _layerIndex = gameObject.layer;
            _defaultLayerName = LayerMask.LayerToName(_layerIndex);
        }

        private void Start()
        {
            ToggleState(false);
        }


        public void ToggleLayer(GameObject targetGameObject)
        {
            if (targetGameObject.layer == LayerMask.NameToLayer(_targetLayerName.ToString()))
            {
                SetLayerRecursively(targetGameObject, _defaultLayerName);
            }
            else
            {
                SetLayerRecursively(targetGameObject, _targetLayerName);
            }
        }

        void SetLayerRecursively(GameObject obj, string layerName)
        {
            int layerIndex = LayerMask.NameToLayer(layerName);

            if (layerIndex != -1)
            {
                obj.layer = layerIndex;
                //Debug.Log("Layer of " + obj.name + " set to: " + layerName);

                // Set the layer for all children
                foreach (Transform child in obj.transform)
                {
                    SetLayerRecursively(child.gameObject, layerName);
                }
            }
            else
            {
                Debug.LogError("Layer not found: " + layerName);
            }
        }

        public void ToggleState(bool flag)
        {
            inspectCamera.enabled = flag;
            inspectCanvas.SetActive(flag);
            effect.SetActive(flag);
        }
    }
}
