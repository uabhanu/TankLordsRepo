using DataSO;
using Events;
using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour
{
    #region Variables

    [SerializeField] private TankData tankData;
    [SerializeField] private TurretData turretData;
    [SerializeField] private UnityEvent onCollected;

    #endregion
    
    #region Functions
    
    private void OnTriggerEnter2D(Collider2D col2D)
    {
        EventsManager.InvokeEvent(TanksEvent.PowerUpCollected , tankData , turretData);
        onCollected?.Invoke();
    }

    #endregion
}
