using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    #region Variables

    private PlayerInputActions _playerInputActions;
    private Vector2 _movementVector;
    private Vector2 _turretMovementVector;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private UnityEvent<Vector2> onMoveBody;
    [SerializeField] private UnityEvent<Vector2> onMoveTurret;
    
    #endregion
    
    #region Functions
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void Start()
    {
        _playerInputActions.Land.Movement.performed += cntxt => _movementVector = cntxt.ReadValue<Vector2>();
        _playerInputActions.Land.Movement.canceled += cntxt => _movementVector = Vector2.zero;

        _playerInputActions.Land.Shoot.performed += GetShootingInput;
        
        _playerInputActions.Land.TurretMovement.performed += cntxt => _turretMovementVector = cntxt.ReadValue<Vector2>();
        _playerInputActions.Land.TurretMovement.canceled += cntxt => _turretMovementVector = Vector2.zero;
    }
    
    private void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
    }

    private void GetBodyMovement()
    {
        onMoveBody?.Invoke(_movementVector.normalized);
    }

    private void GetShootingInput(InputAction.CallbackContext context)
    {
        onShoot?.Invoke();
    }

    private void GetTurretMovement()
    {
        onMoveTurret?.Invoke(_turretMovementVector.normalized);   
    }
    
    #endregion
}
