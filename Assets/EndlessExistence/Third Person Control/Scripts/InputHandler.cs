
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessExistence.Third_Person_Control.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        #region Serialized Private variables
        

        [SerializeField] private InputActionAsset playerControls;


        [SerializeField] private string actionMapName = "Player";


        [SerializeField] private string move = "Move";
        [SerializeField] private string look = "Look";
        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string jump = "Jump";
        [SerializeField] private string interact = "Interact";
        [SerializeField] private string inspect = "Inspect";
        [SerializeField] private string selectionPress = "SelectionPress";
        [SerializeField] private string openInventory = "OpenInventory";

        #endregion


        #region Private variables

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction sprintAction;
        private InputAction jumpAction;
        private InputAction interactAction;
        private InputAction inspectAction;
        private InputAction selectionPressAction;
        private InputAction openInventoryAction;

        #endregion
        
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpTriggered { get; private set; }
        public float SprintValue { get; private set; }
        public bool InteractionTriggered { get; internal set; }
        public bool SelectionTriggered { get; private set; }
        public bool InspectionTriggered { get; internal set; }
        public bool OpenInventoryTriggered { get; internal set; }

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
            selectionPressAction = playerControls.FindActionMap(actionMapName).FindAction(selectionPress);
            inspectAction = playerControls.FindActionMap(actionMapName).FindAction(inspect);
            openInventoryAction = playerControls.FindActionMap(actionMapName).FindAction(openInventory);
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

            selectionPressAction.performed += context => SelectionTriggered = true;
            selectionPressAction.canceled += context => SelectionTriggered = false;

            inspectAction.performed += context => InspectionTriggered = true;
            inspectAction.canceled += context => InspectionTriggered = false;
            
            openInventoryAction.performed += context => OpenInventoryTriggered = true;
            openInventoryAction.canceled += context => OpenInventoryTriggered = false;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            sprintAction.Enable();
            jumpAction.Enable();
            interactAction.Enable();
            selectionPressAction.Enable();
            inspectAction.Enable();
            openInventoryAction.Enable();
            
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            sprintAction.Disable();
            jumpAction.Disable();
            interactAction.Disable();
            selectionPressAction.Disable();
            inspectAction.Disable();
            openInventoryAction.Disable();
        }
    }
}
