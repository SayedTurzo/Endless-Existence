using UnityEngine;

namespace __EndlessExistence.Item_Interaction.Scripts
{
    public class InteractionSensor : MonoBehaviour
    {
        private EE_Item _parent;
        private string playerTag;

        private void Awake()
        {
            _parent = transform.parent.gameObject.GetComponent<EE_Item>();
            playerTag = _parent.playerTag;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                _parent.TriggerCanvas();
                _parent.TriggerEffect();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                _parent.TriggerCanvas();
                _parent.TriggerEffect();
            }
        }
    }
}
