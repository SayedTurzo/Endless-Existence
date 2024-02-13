using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found in the scene.");
        }
    }

    void Update()
    {
        void Update()
        {
            // Check if the main camera reference is available
            if (mainCamera != null)
            {
                // Calculate the direction from the UI element to the camera
                Vector3 lookAtDirection = mainCamera.transform.position - transform.position;

                // Ensure the UI element always faces the camera
                transform.rotation = Quaternion.LookRotation(lookAtDirection, Vector3.up);
            }
        }
    }
}
