using System;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomInspector;

namespace _Small_Projects._DependencyInjection_tests.Unity_Dependecy_Injection.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        #region Serialized Private variables
        
        [HorizontalLine("Input Action Asset",3,FixedColor.Blue)]
        [SerializeField] private InputActionAsset playerControls;

        [HorizontalLine("Action Map Name References",3,FixedColor.Green)] 
        [SerializeField] private string actionMapName = "Player";

        [HorizontalLine("Action Name References", 3, FixedColor.Yellow)] 
        [SerializeField] private string move = "Move";
        [SerializeField] private string look = "Look";
        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string jump = "Jump";

        #endregion


        #region Private variables

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction sprintAction;
        private InputAction jumpAction;

        #endregion
        
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpTriggerd { get; private set; }
        public float SprintValue { get; private set; }
        
        public static InputHandler Instance { get; private set; }

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

            moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
            lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
            sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
            jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
            RegisterInputActions();
        }

        private void RegisterInputActions()
        {
            moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
            moveAction.canceled += context => MoveInput = Vector2.zero;
            
            lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
            lookAction.canceled += context => LookInput = Vector2.zero;
            
            sprintAction.performed += context => SprintValue = context.ReadValue<float>();
            sprintAction.canceled += context => SprintValue = 0;
            
            jumpAction.performed += context => JumpTriggerd = true;
            jumpAction.canceled += context => JumpTriggerd = false;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            sprintAction.Enable();
            jumpAction.Enable();
            
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            sprintAction.Disable();
            jumpAction.Disable();
        }
    }
}
