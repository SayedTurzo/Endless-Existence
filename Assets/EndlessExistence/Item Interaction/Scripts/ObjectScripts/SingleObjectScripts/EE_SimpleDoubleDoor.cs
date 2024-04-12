using System.Collections;
using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts.SingleObjectScripts
{
    public class EE_SimpleDoubleDoor : ObjectContainer
    {
        public Transform leftRotatablePart; // Reference to the left Transform responsible for rotating the door

        public Transform rightRotatablePart; // Reference to the right Transform responsible for rotating the door
        public float openAngle = 45f; // Angle to open the door (set in the inspector)
        public float smoothSpeed = 5f; // Speed of door rotation

        private Quaternion leftClosedRotation; // Closed rotation of the left door part
        private Quaternion rightClosedRotation; // Closed rotation of the right door part
        private Quaternion leftOpenRotation; // Open rotation of the left door part
        private Quaternion rightOpenRotation; // Open rotation of the right door part

        private bool isOpen = false; // Flag to track if the door is open

        void Start()
        {
            // Store the closed rotations of the door parts
            leftClosedRotation = leftRotatablePart.rotation;
            rightClosedRotation = rightRotatablePart.rotation;

            // Calculate the open rotations based on the closed rotations and openAngle
            leftOpenRotation = leftClosedRotation * Quaternion.Euler(0f, openAngle, 0f);
            rightOpenRotation = rightClosedRotation * Quaternion.Euler(0f, -openAngle, 0f);
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
                StartCoroutine(RotateDoors(leftRotatablePart, leftClosedRotation));
                StartCoroutine(RotateDoors(rightRotatablePart, rightClosedRotation));
            }
            else
            {
                StartCoroutine(RotateDoors(leftRotatablePart, leftOpenRotation));
                StartCoroutine(RotateDoors(rightRotatablePart, rightOpenRotation));
            }

            isOpen = !isOpen;
        }

        IEnumerator RotateDoors(Transform doorPart, Quaternion targetRotation)
        {
            float elapsedTime = 0f;
            Quaternion startRotation = doorPart.rotation;

            while (elapsedTime < smoothSpeed)
            {
                doorPart.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / smoothSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            doorPart.rotation = targetRotation;
        }
    }
}
