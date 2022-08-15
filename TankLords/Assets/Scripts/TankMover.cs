using DataSO;
using UnityEngine;
using UnityEngine.Events;

public class TankMover : MonoBehaviour
{
    #region Variables
    
    private float _currentForwardDirection = 1f;
    private float _currentSpeed = 0f;
    private Vector2 _movementVector;

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private TankData tankData;
    [SerializeField] private UnityEvent<float> onSpeedChange = new UnityEvent<float>();

    #endregion

    #region Functions

    private void FixedUpdate()
    {
        rb2D.velocity = (Vector2)transform.up * (_currentForwardDirection * _currentSpeed * Time.fixedDeltaTime);
        rb2D.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * tankData.RotationSpeed * Time.fixedDeltaTime));
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
        onSpeedChange?.Invoke(movementVector.magnitude);

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
