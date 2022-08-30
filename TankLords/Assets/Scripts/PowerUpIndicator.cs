using DataSO;
using Events;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpIndicator : MonoBehaviour
{
    #region Variables
    
    private bool _powerUpTaken;
    private float _currentDelay;
    private float _powerUpTime = 15f;

    [SerializeField] private UnityEvent<float> onPowerUpTaken;

    public float CurrentDelay => _currentDelay;

    public float PowerUpTime => _powerUpTime;

    #endregion

    #region Functions
    
    private void Start()
    {
        onPowerUpTaken?.Invoke(_currentDelay);
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void Update()
    {
        if(_powerUpTaken)
        {
            _currentDelay -= Time.deltaTime;
            onPowerUpTaken?.Invoke(_currentDelay /_powerUpTime);   
        }

        if(_currentDelay <= 0)
        {
            onPowerUpTaken?.Invoke(_currentDelay);
            _powerUpTaken = false;
        }
    }

    private void OnPowerUpTaken(TankData noUseForTankData , TurretData noUseForTurretData)
    {
        _currentDelay = _powerUpTime;
        onPowerUpTaken?.Invoke(_currentDelay);
        _powerUpTaken = true;
    }
    
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.PowerUpCollected , OnPowerUpTaken);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.PowerUpCollected , OnPowerUpTaken);
    }
    
    #endregion
}
