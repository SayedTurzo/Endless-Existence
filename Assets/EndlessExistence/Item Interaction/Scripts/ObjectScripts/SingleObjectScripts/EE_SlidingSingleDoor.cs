using System.Collections;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_SlidingSingleDoor : ObjectContainer
    {
        public Transform slidingPart;   // Reference to the Transform responsible for sliding the door
        public float openDistance = 2f; // Distance to slide the door (set in the inspector)
        public float smoothSpeed = 5f;  // Speed of door sliding

        private Vector3 closedPosition; // Closed position of the door
        private Vector3 openPosition;   // Open position of the door

        private bool isOpen = false; // Flag to track if the door is open

        void Start()
        {
            // Store the closed position of the door
            closedPosition = slidingPart.localPosition;

            // Calculate the open position based on the closed position and openDistance
            openPosition = closedPosition - new Vector3(openDistance, 0f, 0f);
        }

        public override void Interact()
        {
            ControlDoor();
            Debug.Log("triggered");
        }

        private void ControlDoor()
        {
            StartCoroutine(SlideDoor(isOpen ? closedPosition : openPosition));
            isOpen = !isOpen;
        }

        IEnumerator SlideDoor(Vector3 targetPosition)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = slidingPart.localPosition;

            while (elapsedTime < smoothSpeed)
            {
                slidingPart.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / smoothSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            slidingPart.localPosition = targetPosition;
        }
    }
}
