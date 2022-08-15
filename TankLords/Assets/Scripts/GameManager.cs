using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    private GameObject _player;
    private SaveSystem _saveSystem;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += Initialize;
    }

    private void Initialize(Scene scene , LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded Game Manager");

        var playerInput = FindObjectOfType<PlayerInput>();

        if(playerInput != null)
        {
            _player = playerInput.gameObject;
        }

        _saveSystem = FindObjectOfType<SaveSystem>();

        if(_player != null && _saveSystem.LoadedData != null)
        {
            var damageable = _player.GetComponentInChildren<Damageable>();
            damageable.Health = _saveSystem.LoadedData.PlayerHealth;
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
            _saveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1 , _player.GetComponentInChildren<Damageable>().Health);
        }
    }

    #endregion
}