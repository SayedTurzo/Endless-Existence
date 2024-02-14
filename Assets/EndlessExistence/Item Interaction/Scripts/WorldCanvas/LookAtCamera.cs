using UnityEngine;

namespace EndlessExistence.Item_Interaction.Scripts.WorldCanvas
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            var rotation = _camera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward , rotation*Vector3.up);
        }
    }
}
