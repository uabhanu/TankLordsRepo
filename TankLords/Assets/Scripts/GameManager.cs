using AI;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    private GameObject _player;
    private int _minimumNumberOfEnemies = 0;
    private int _numberOfEnemyTanksAlive;
    private SaveSystem _saveSystem;
    private ScoreManager _scoreManager;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += Initialize;
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    private void Initialize(Scene scene , LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded Game Manager");

        var playerInput = FindObjectOfType<PlayerInput>();

        if(playerInput != null)
        {
            _numberOfEnemyTanksAlive = FindObjectsOfType<DefaultEnemyAI>().Length;
            _player = playerInput.gameObject;
            _scoreManager = FindObjectOfType<ScoreManager>();
        }

        _saveSystem = FindObjectOfType<SaveSystem>();

        if(_player != null && _saveSystem.LoadedData != null)
        {
            var damageable = _player.GetComponentInChildren<Damageable>();
            damageable.Health = _saveSystem.LoadedData.PlayerHealth;
            _scoreManager.HighScore = _saveSystem.LoadedData.HighScore;
            _scoreManager.MoneyEarnedScore = _saveSystem.LoadedData.MoneyEarned;
        }
    }

    public void LoadLevel()
    {
        if(_saveSystem.LoadedData != null)
        {
            SceneManager.LoadScene(_saveSystem.LoadedData.SceneIndex);
            return;
        }
        
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SaveData()
    {
        if(_player != null)
        {
            _saveSystem.SaveData(_scoreManager.HighScore , _scoreManager.MoneyEarnedScore , SceneManager.GetActiveScene().buildIndex + 1 , _player.GetComponentInChildren<Damageable>().Health);
        }
    }

    #endregion
    
    #region Event Functions

    private void OnEnemyDied()
    {
        _numberOfEnemyTanksAlive--;
        Mathf.Clamp(_numberOfEnemyTanksAlive , _minimumNumberOfEnemies , _numberOfEnemyTanksAlive);

        if(_numberOfEnemyTanksAlive == _minimumNumberOfEnemies)
        {
            GetComponent<ObjectGeneratorUtil>().CreateObject();
        }
    }
    
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(TanksEvent.EnemyDied , OnEnemyDied);
    }
    
    private void UnsubscribeToEvents()
    {
        EventsManager.UnsubscribeFromEvent(TanksEvent.EnemyDied , OnEnemyDied);
    }
    
    #endregion
}