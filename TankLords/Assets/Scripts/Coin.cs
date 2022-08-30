using DataSO;
using Events;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    [SerializeField] private CoinData coinData;
    [SerializeField] private UnityEvent onCollected;
    
    private void OnTriggerEnter2D(Collider2D col2D)
    {
        EventsManager.InvokeEvent(TanksEvent.CoinCollected , clipToPlay , coinData);
        onCollected?.Invoke();
    }
}
