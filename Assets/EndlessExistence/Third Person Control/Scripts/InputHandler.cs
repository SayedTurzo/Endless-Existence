using CustomInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessExistence.Third_Person_Control.Scripts
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
        [SerializeField] private string interact = "Interact";

        #endregion


        #region Private variables

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction sprintAction;
        private InputAction jumpAction;
        private InputAction interactAction;

        #endregion
        
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpTriggered { get; private set; }
        public float SprintValue { get; private set; }
        public bool InteractionTriggered { get; private set; }
        
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
            interactAction = playerControls.FindActionMap(actionMapName).FindAction(interact);
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
            
            jumpAction.performed += context => JumpTriggered = true;
            jumpAction.canceled += context => JumpTriggered = false;

            interactAction.performed += context => InteractionTriggered = true;
            interactAction.canceled += context => InteractionTriggered = false;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            sprintAction.Enable();
            jumpAction.Enable();
            interactAction.Enable();
            
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            sprintAction.Disable();
            jumpAction.Disable();
            interactAction.Disable();
        }
    }
}
