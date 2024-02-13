using UnityEngine;

public class InteractionSensor : MonoBehaviour
{
    private Interactable _parent;

    private void Awake()
    {
        _parent = transform.parent.gameObject.GetComponent<Interactable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatsModel playerStats = other.GetComponent<PlayerStatsModel>();
            if (playerStats != null)
            {
                _parent.Use(playerStats);
                //Destroy(gameObject);
            }
            _parent.TriggerUI();
            _parent.Interact();
            _parent.TriggerEffect();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _parent.gameObject.GetComponent<Interactable>().TriggerUI();
            _parent.gameObject.GetComponent<Interactable>().TriggerEffect();
        }
    }
}
