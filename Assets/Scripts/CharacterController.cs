using Input;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterController : MonoBehaviour
{
    [SerializeField] InputReader input;
    //---------------------------------
    [Header("Head Physics")]
    [SerializeField] GameObject head; // Reference to the Head GameObject
    [Header("Foot Physics")]
    [SerializeField] float legForce;
    [SerializeField] float legCooldownTime = 0.2f; // Cooldown time between leg movements
    [SerializeField] Rigidbody rightFoot, leftFoot;
    [Header("Arm Physics")]
    [SerializeField] float armAttackForce;
    [SerializeField] Rigidbody rightArm, leftArm;
    //---------------------------------
    [Header("Movement Settings")]
    [SerializeField] Vector3 bodyTorque;
    [SerializeField] Vector3 movementDirectionForce;
    [SerializeField] Rigidbody body;
    //---------------------------------
    ConstantForce _headConstantForce;
    
    Vector3 _initialHeadForce;
    Vector3 _movementInput;

    float _nextLegMoveTime;
        
    bool _wasLastLegRight;
    bool _leftAttackTriggered;
    bool _rightAttackTriggered;

    void Awake()
    {
        _headConstantForce = head.GetComponent<ConstantForce>();
        _initialHeadForce = _headConstantForce.force; // Store the initial force
    }
    void Start()
    {
        // Initialize the input reader
        //TODO: Implement OnEnable and OnDisable methods to enable/disable input and unsubscribe from events (to avoid memory leaks)
        input.MoveEvent += direction => _movementInput = direction;
        input.JumpEvent += Jump;
        input.LeftArmAttackEvent += isLeftArmAttackPressed =>
        {
            _leftAttackTriggered = isLeftArmAttackPressed;
        };
        input.RightArmAttackEvent += isRightArmAttackPressed =>
        {
            _rightAttackTriggered = isRightArmAttackPressed;
        };
        input.EnablePlayerActions();
    }
    void Update()
    {
        _movementInput = NormalizeInput(_movementInput);
    }
    void FixedUpdate()
    {
        Move(_movementInput);

        if (_leftAttackTriggered)
        {
            LeftArmAttack();
        }
        if (_rightAttackTriggered)
        {
            RightArmAttack();
        }
    }
    
    #region Handling
    Vector3 NormalizeInput(Vector3 moveInput)
    {
        // Normalize the input to ensure consistent movement magnitude for rotation
        //TODO: add camera direction into calculation
        return moveInput.normalized;
    }
    
    //TODO: Refactor this to use the chain of responsibility pattern (too mitigate all the if statements).
    void Move(Vector3 moveInputNormalized)
    {
        if (Time.time >= _nextLegMoveTime) // Check if cooldown has elapsed
        {
            if (moveInputNormalized.y != 0) // Forward or backward movement
            {
                int directionMultiplier = moveInputNormalized.y > 0 ? 1 : -1; // Determine direction
                ApplyLegForce(directionMultiplier);
            }
        }

        if (moveInputNormalized.x == 0)
            return;
        
        // Rotate the body
        Vector3 rotation;
        if (moveInputNormalized.x > 0)
            rotation = bodyTorque;
        else
            rotation = -bodyTorque;
        body.transform.eulerAngles += rotation;
    }
    void ApplyLegForce(int directionMultiplier)
    {
        var calculatedLegForce = movementDirectionForce * (legForce * directionMultiplier);
    
        if (_wasLastLegRight)
        {
            rightFoot.AddRelativeForce(calculatedLegForce, ForceMode.Impulse);
        }
        else
        {
            leftFoot.AddRelativeForce(calculatedLegForce, ForceMode.Impulse);
        }
    
        _wasLastLegRight = !_wasLastLegRight; // Toggle the leg
        _nextLegMoveTime = Time.time + legCooldownTime; // Reset cooldown
    }
    void Jump(bool isJumpPressed)
    {
        if (isJumpPressed)
        {
            // On Jump Pressed: set Y force to 0
            Vector3 force = _headConstantForce.force;
            force.y = 0;
            _headConstantForce.force = force;
        }
        else
        {
            // On Jump Released: Restore initial force
            _headConstantForce.force = _initialHeadForce; // Restore the initial force
        }
    }
    void RightArmAttack()
    {
        var attack = Vector3.forward * Random.value;
        rightArm.AddRelativeForce(attack * armAttackForce, ForceMode.Impulse);
    }
    void LeftArmAttack()
    {
        var attack = Vector3.forward * Random.value;
        leftArm.AddRelativeForce(attack * armAttackForce, ForceMode.Impulse);
    }
  #endregion
    
}