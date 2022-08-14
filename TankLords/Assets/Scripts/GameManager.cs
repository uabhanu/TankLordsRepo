using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    public GameObject Player;
    public SaveSystem SaveSystem;
    
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
            Player = playerInput.gameObject;
        }

        SaveSystem = FindObjectOfType<SaveSystem>();

        if(Player != null && SaveSystem.LoadedData != null)
        {
            var damageable = Player.GetComponentInChildren<Damageable>();
            damageable.Health = SaveSystem.LoadedData.PlayerHealth;
        }
    }

    public void LoadLevel()
    {
        if(SaveSystem.LoadedData != null)
        {
            SceneManager.LoadScene(SaveSystem.LoadedData.SceneIndex);
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
        if(Player != null)
        {
            SaveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1 , Player.GetComponentInChildren<Damageable>().Health);
        }
    }

    #endregion
}