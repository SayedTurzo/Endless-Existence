using System.Collections;
using CustomInspector;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_SimpleDoorObject : ObjectContainer
    {
        [HorizontalLine("Door References", 2, FixedColor.Yellow)]
        public Transform rotatablePart; // Reference to the Transform responsible for rotating the door
        public float openAngle = 45f; // Angle to open the door (set in the inspector)
        public float smoothSpeed = 5f; // Speed of door rotation
        private Quaternion initialRotation; // Initial rotation of the door

        private bool isOpen = false; // Flag to track if the door is open

        void Start()
        {
            // Store the initial rotation of the door
            initialRotation = rotatablePart.rotation;
        }
        
        
        
        public override void Interact()
        {
            ControlDoor();
            Debug.Log("triggered");

        }

        private void ControlDoor()
        {
            StartCoroutine(isOpen
                ? RotateDoor(initialRotation)
                : RotateDoor(initialRotation * Quaternion.Euler(0f, openAngle, 0f)));
        }
        
        
        IEnumerator RotateDoor(Quaternion targetRotation)
        {
            float elapsedTime = 0f;
            Quaternion startRotation = rotatablePart.rotation;
            
            while (elapsedTime < smoothSpeed)
            {
                rotatablePart.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / smoothSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rotatablePart.rotation = targetRotation;

            isOpen = !isOpen;
            if (autoInteract)
            {
                initialRotation = startRotation;
                //startRotation = rotatablePart.rotation;
            }
        }
    }
}