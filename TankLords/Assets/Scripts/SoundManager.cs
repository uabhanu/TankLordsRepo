using DataSO;
using Events;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private AudioSource audioSource;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void OnCoinCollected(AudioClip clipToPlay , CoinData noUseForCoinData)
    {
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.CoinCollected , OnCoinCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.CoinCollected , OnCoinCollected);
    }
    
    #endregion
}
