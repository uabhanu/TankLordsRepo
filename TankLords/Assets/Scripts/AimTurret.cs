using DataSO;
using Events;
using System.Collections;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    #region Variables

    private PowerUpIndicator _powerUpIndicator;
    private TurretData _currentTurretData;
    
    [SerializeField] private TurretData turretData;
    
    public TurretData CurrentTurretData => _currentTurretData;

    #endregion

    #region Functions

    private void Awake()
    {
        _currentTurretData = turretData;
        _powerUpIndicator = GetComponentInParent<PowerUpIndicator>();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    
    private IEnumerator PowerUpCoroutine()
    {
        yield return new WaitForSeconds(_powerUpIndicator.PowerUpTime);
        ResetData();
    }

    private void OnPowerUpCollected(TankData noTankDataRequired , TurretData powerUpTurretData)
    {
        TankController tankController = GetComponentInParent<TankController>();
        
        if(!tankController.IsEligibleForPowerUps)
        {
            return;
        }
        
        _currentTurretData = powerUpTurretData; //Can't see this in the inspector so testing will be tricky. It is working thankfully
        StartCoroutine(PowerUpCoroutine());
    }
    
    private void ResetData()
    {
        _currentTurretData = turretData;
    }

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.PowerUpCollected , OnPowerUpCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.PowerUpCollected , OnPowerUpCollected);
    }
    
    public void Aim(Vector2 inputPointerPosition)
    {
        var turretDirection = (Vector3)inputPointerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y , turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretData.RotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation , Quaternion.Euler(0 , 0 , desiredAngle) , rotationStep);
    }

    #endregion
}
