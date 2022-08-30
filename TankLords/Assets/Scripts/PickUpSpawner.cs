using DataSO;
using Events;
using System.Collections;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    #region Variables
    
    private int _powerUp;
    
    [SerializeField] private float powerUpDelay;
    [SerializeField] private PowerUpSpawnData powerUpSpawnData;
    
    #endregion

    #region Functions
    
    private void Start()
    {
        _powerUp = 0;
        StartCoroutine(PowerUpTimer());
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = powerUpSpawnData.GizmosColour;
        Gizmos.DrawWireSphere(transform.position , powerUpSpawnData.Radius);
    }

    private IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(powerUpDelay);

        var i = Random.Range(0 , powerUpSpawnData.AvailablePowerUps.Length);

        if(_powerUp == 0)
        {
            Instantiate(powerUpSpawnData.AvailablePowerUps[i] , GetRandomPosition()  , Quaternion.identity);
            _powerUp++;
        }

        StartCoroutine(PowerUpTimer());
    }

    private void OnCollected(TankData noNeedForTankData , TurretData noNeedForTurretData)
    {
        _powerUp--;
    }

    //This is redundant so find a better way to achieve this
    private void OnMedicCollected()
    {
        _powerUp--;
    }
    
    private Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * powerUpSpawnData.Radius + (Vector2)transform.position;
    }
    
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.MedicCollected , OnMedicCollected);
        EventsManager.SubscribeToEvent(TanksEvent.PowerUpCollected , OnCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.MedicCollected , OnMedicCollected);
        EventsManager.UnsubscribeFromEvent(TanksEvent.PowerUpCollected , OnCollected);
    }
    
    #endregion
}
