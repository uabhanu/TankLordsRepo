using DataSO;
using Events;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Variables
    
    private int _moneyEarned;

    [SerializeField] private TMP_Text moneyEarnedValueDisplayTMP;

    #endregion

    #region Functions
    
    private void Awake()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsbscribeFromEvents();
    }

    private void OnCoinCollected(AudioClip noUseForAudioClip , CoinData coinData)
    {
        _moneyEarned += coinData.CoinValue;
        moneyEarnedValueDisplayTMP.text = _moneyEarned.ToString();
    }

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.CoinCollected , OnCoinCollected);
    }
    
    private void UnsbscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.CoinCollected , OnCoinCollected);
    }
    
    public int MoneyEarnedScore
    {
        get => _moneyEarned;

        set
        {
            _moneyEarned = value;
            moneyEarnedValueDisplayTMP.text = _moneyEarned.ToString();
        }
    }
    
    #endregion
}
