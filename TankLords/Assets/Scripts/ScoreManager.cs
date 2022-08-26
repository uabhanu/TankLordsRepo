using DataSO;
using Events;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Variables

    private int _highScore;
    private int _moneyEarnedScore;
    
    [SerializeField] private TMP_Text highScoreValueDisplayTMP;
    [SerializeField] private TMP_Text moneyEarnedValueDisplayTMP;
    [SerializeField] private TankData tankData;

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
        _moneyEarnedScore += coinData.CoinValue;
        moneyEarnedValueDisplayTMP.text = _moneyEarnedScore.ToString();
    }

    private void OnEnemyDied() //TODO Try doing OnCinCollected also this way
    {
        _highScore += tankData.KillValue;
        highScoreValueDisplayTMP.text = _highScore.ToString();
    }

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.CoinCollected , OnCoinCollected);
        EventsManager.SubscribeToEvent(TanksEvent.EnemyDied , OnEnemyDied);
    }
    
    private void UnsbscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.CoinCollected , OnCoinCollected);
        EventsManager.UnsubscribeFromEvent(TanksEvent.EnemyDied , OnEnemyDied);
    }
    
    public int HighScore
    {
        get => _highScore;
        
        set
        {
            _highScore = value;
            highScoreValueDisplayTMP.text = _highScore.ToString();
        }
    }
    
    public int MoneyEarnedScore
    {
        get => _moneyEarnedScore;

        set
        {
            _moneyEarnedScore = value;
            moneyEarnedValueDisplayTMP.text = _moneyEarnedScore.ToString();
        }
    }

    #endregion
}
