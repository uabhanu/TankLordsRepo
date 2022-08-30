using DataSO;
using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TankMover : MonoBehaviour
{
    #region Variables
    
    private float _currentForwardDirection = 1f;
    private float _currentSpeed;
    private PowerUpIndicator _powerUpIndicator;
    private TankData _currentTankData;
    private Vector2 _movementVector;
    
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private TankData tankData;
    [SerializeField] private UnityEvent<float> onSpeedChange = new UnityEvent<float>();

    #endregion

    #region Functions

    private void Awake()
    {
        _currentTankData = tankData;
        _powerUpIndicator = GetComponentInParent<PowerUpIndicator>();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void FixedUpdate()
    {
        rb2D.velocity = (Vector2)transform.up * (_currentForwardDirection * _currentSpeed * Time.fixedDeltaTime);
        rb2D.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * _currentTankData.RotationSpeed * Time.fixedDeltaTime));
    }

    private IEnumerator PowerUpCoroutine()
    {
        yield return new WaitForSeconds(_powerUpIndicator.PowerUpTime);
        ResetData();
    }

    private void CalculateCurrentSpeed(Vector2 movementVector)
    {
        if(Mathf.Abs(movementVector.y) > 0)
        {
            _currentSpeed += _currentTankData.Acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _currentTankData.Deacceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed , 0f , _currentTankData.MaxSpeed);
    }

    private void OnPowerUpCollected(TankData boostMovementData , TurretData noTurretDataRequired)
    {
        TankController tankController = GetComponentInParent<TankController>();
        
        if(!tankController.IsEligibleForPowerUps)
        {
            return;
        }
        
        _currentTankData = boostMovementData; //Can't see this in the inspector so testing will be tricky. It is working thankfully
        StartCoroutine(PowerUpCoroutine());
    }

    private void ResetData()
    {
        _currentTankData = tankData;
    }

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.PowerUpCollected , OnPowerUpCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.PowerUpCollected , OnPowerUpCollected);
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
