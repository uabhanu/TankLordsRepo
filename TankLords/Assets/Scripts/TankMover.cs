using DataSO;
using UnityEngine;
using UnityEngine.Events;

public class TankMover : MonoBehaviour
{
    #region Variables
    
    private float _currentForwardDirection = 1f;
    private float _currentSpeed = 0f;
    private Vector2 _movementVector;

    public Rigidbody2D Rb2d;
    public TankData tankData;
    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    #endregion

    #region Functions
    
    private void Awake()
    {
        Rb2d = GetComponentInParent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        Rb2d.velocity = (Vector2)transform.up * (_currentForwardDirection * _currentSpeed * Time.fixedDeltaTime);
        Rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * tankData.RotationSpeed * Time.fixedDeltaTime));
    }

    private void CalculateCurrentSpeed(Vector2 movementVector)
    {
        if(Mathf.Abs(movementVector.y) > 0)
        {
            _currentSpeed += tankData.Acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= tankData.Deacceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed , 0f , tankData.MaxSpeed);
    }

    public void Move(Vector2 movementVector)
    {
        _movementVector = movementVector;
        CalculateCurrentSpeed(movementVector);
        OnSpeedChange?.Invoke(movementVector.magnitude);

        if(movementVector.y > 0)
        {
            _currentForwardDirection = 1f;
        }
        
        else if(movementVector.y < 0)
        {
            _currentForwardDirection = -1f;
        }
    }
    
    #endregion
}
