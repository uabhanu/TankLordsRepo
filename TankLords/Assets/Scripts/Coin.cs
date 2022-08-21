using DataSO;
using Events;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    [SerializeField] private CoinData coinData;
    
    private void OnTriggerEnter2D(Collider2D col2D)
    {
        EventsManager.InvokeEvent(TanksEvent.CoinCollected , clipToPlay , coinData);
        Destroy(gameObject);
    }
}
