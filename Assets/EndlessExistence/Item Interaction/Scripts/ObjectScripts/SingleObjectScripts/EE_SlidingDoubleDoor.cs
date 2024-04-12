using System.Collections;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_SlidingDoubleDoor : ObjectContainer
    {
        public Transform leftSlidingPart;  // Reference to the left Transform responsible for sliding the door
        public Transform rightSlidingPart; // Reference to the right Transform responsible for sliding the door
        public float openDistance = 2f;    // Distance to slide the door (set in the inspector)
        public float smoothSpeed = 5f;     // Speed of door sliding

        private Vector3 leftClosedPosition;  // Closed position of the left door part
        private Vector3 rightClosedPosition; // Closed position of the right door part
        private Vector3 leftOpenPosition;    // Open position of the left door part
        private Vector3 rightOpenPosition;   // Open position of the right door part

        private bool isOpen = false; // Flag to track if the door is open

        void Start()
        {
            // Store the closed positions of the door parts
            leftClosedPosition = leftSlidingPart.localPosition;
            rightClosedPosition = rightSlidingPart.localPosition;

            // Calculate the open positions based on the closed positions and openDistance
            leftOpenPosition = leftClosedPosition - new Vector3(openDistance, 0f, 0f);
            rightOpenPosition = rightClosedPosition + new Vector3(openDistance, 0f, 0f);
        }

        public override void Interact()
        {
            ControlDoor();
            Debug.Log("triggered");
        }

        private void ControlDoor()
        {
            if (isOpen)
            {
                StartCoroutine(SlideDoors(leftSlidingPart, leftClosedPosition));
                StartCoroutine(SlideDoors(rightSlidingPart, rightClosedPosition));
            }
            else
            {
                StartCoroutine(SlideDoors(leftSlidingPart, leftOpenPosition));
                StartCoroutine(SlideDoors(rightSlidingPart, rightOpenPosition));
            }

            isOpen = !isOpen;
        }

        IEnumerator SlideDoors(Transform doorPart, Vector3 targetPosition)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = doorPart.localPosition;

            while (elapsedTime < smoothSpeed)
            {
                doorPart.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / smoothSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            doorPart.localPosition = targetPosition;
        }
    }
}
