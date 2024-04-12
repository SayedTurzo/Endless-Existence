
using UnityEngine;

namespace EndlessExistence.Third_Person_Control.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThirdPersonCharacterController : MonoBehaviour
    {
        #region variables


        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _runSpeed = 10f;
        [SerializeField] private float _jumpHeight = 2f;
        [Range(0f, 1f)] [SerializeField] private float _smoothFacing = 0.75f;
        private bool _isRunning;


        [SerializeField] private float _gravityScale = 2f;
        private const float _GRAVITY = -9.8f;


        [SerializeField] private float _groundCheckRadius = 0.1f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private bool _isGrounded;

        // component variables
        private Transform _cameraTransform;
        private Rigidbody _rigidbody;

        private int _remainingJumps;
        public int maxJumps = 2; // You can set this value to control the number of jumps
        private int _additionalJumps;
    
        [SerializeField, Range(0.01f, 1f)]
        private float _gizmoLineThickness = 0.1f;

        #endregion

        /*----------------------------------------------------------------------------------*/

        #region unity methods

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;   // get camera transform
            _rigidbody = GetComponent<Rigidbody>();     // get character controller component
            _remainingJumps = maxJumps;
            _additionalJumps = 0;
            
            //FindAndPrintObjectNameWithAudioSource();
        }
        
        // void FindAndPrintObjectNameWithAudioSource()
        // {
        //     // Find all GameObjects with an AudioSource component in the scene
        //     AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        //
        //     // Check if any AudioSource components were found
        //     if (audioSources.Length > 0)
        //     {
        //         // Print the name of the first GameObject with an AudioSource component
        //         Debug.Log("Object with AudioSource: " + audioSources[0].gameObject.name);
        //     }
        //     else
        //     {
        //         Debug.Log("No object with AudioSource found in the scene.");
        //     }
        // }

        private void Update()
        {
            // handle inputs
            HandleInput();
        }

        private void FixedUpdate()
        {
            // ground check
            HandleGroundCheck(_groundCheckRadius, _groundLayer);

            // handle gravity
            HandleGravity(_GRAVITY, _gravityScale);

            // handle facing roatation
            HandleFacingRotation(_smoothFacing);

            // move the character
            HandleMovement(_moveSpeed, _runSpeed);

        }
    
        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Vector3 point1 = transform.position + Vector3.up * _groundCheckRadius / 2;
            Vector3 point2 = transform.position - Vector3.up * _groundCheckRadius / 2;
        
            Gizmos.DrawWireSphere(point1, _groundCheckRadius / 2);
            Gizmos.DrawWireSphere(point2, _groundCheckRadius / 2);

            bool isCapsuleCastHit = Physics.CapsuleCast(point1, point2, _groundCheckRadius, Vector3.down, 0.1f, _groundLayer);
        
            if (isCapsuleCastHit)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
#endif
        }

        #endregion

        /*----------------------------------------------------------------------------------*/

        #region handler methods

        private void HandleInput()
        {
            // WASD input using InputHandler
            _isRunning = InputHandler.Instance.SprintValue > 0;

            if (_isGrounded)
            {
                // jump using InputHandler
                if (InputHandler.Instance.JumpTriggered)
                    Jump(_jumpHeight, _GRAVITY, _gravityScale);
            }       
        }

        private void HandleGroundCheck(float groundCheckRadius, LayerMask groundLayer)
        {
            Vector3 point1 = transform.position + Vector3.up * _groundCheckRadius / 2;
            Vector3 point2 = transform.position - Vector3.up * _groundCheckRadius / 2;

            _isGrounded = Physics.CheckCapsule(point1, point2, _groundCheckRadius, groundLayer);

            // Debug information
            if (_isGrounded)
            {
                //Debug.Log("Grounded");
            }
            else
            {
                //Debug.Log("Not Grounded");
            }
        }

        private void HandleGravity(float gravityPower, float gravityScale)
        {
            _rigidbody.AddForce(Vector3.up * gravityPower * gravityScale, ForceMode.Acceleration);
        }

        private void HandleFacingRotation(float smoothRotation)
        {
            if (_isGrounded)
            {
                if (RelativeDirection().magnitude > 0)
                {
                    transform.rotation = Quaternion.Lerp
                    (
                        transform.rotation,
                        Quaternion.LookRotation(RelativeDirection()),
                        smoothRotation * 10f * Time.deltaTime
                    );
                }
            }
        }

        private void HandleMovement(float moveSpeed, float runSpeed)
        {
            // Reset jumps when grounded
            _remainingJumps = maxJumps;
            //if (!_isGrounded) return;
            if (_isRunning)
                Move(runSpeed);
            else
                Move(moveSpeed);
        }

        #endregion

        /*----------------------------------------------------------------------------------*/

        #region movement methods

        private Vector3 RelativeDirection()
        {
            Vector3 zDir = _cameraTransform.forward;
            Vector3 xDir = _cameraTransform.right;
            zDir.y = 0;
            xDir.y = 0;

            return (zDir * InputHandler.Instance.MoveInput.y + xDir * InputHandler.Instance.MoveInput.x).normalized;
        }

        private void Move(float moveSpeed)
        {
            _rigidbody.velocity = new Vector3(
                RelativeDirection().x * moveSpeed,
                _rigidbody.velocity.y,
                RelativeDirection().z * moveSpeed);
        }

        private void Jump(float jumpheight, float gravity, float gravityScale)
        {
            if(_isGrounded)
            {
                _rigidbody.velocity = new Vector3(
                    _rigidbody.velocity.x,
                    Mathf.Sqrt(jumpheight * -2 * (gravity * gravityScale)),
                    _rigidbody.velocity.z);

                if(RelativeDirection().magnitude > 0)
                    transform.rotation = Quaternion.LookRotation(RelativeDirection());
            }
        }

        #endregion
    }
}
