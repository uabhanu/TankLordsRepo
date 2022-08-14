using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    #region Variables
    
    private bool _isInitialized;
    
    public LoadedData LoadedData
    {
        get;
        private set;
    }

    public string PlayerHealthKey = "PlayerHealth";
    public string SavePresentKey = "SavePresent";
    public string SceneKey = "SceneIndex";

    public UnityEvent<bool> OnDataLoadedResult;
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var result = LoadData();
        OnDataLoadedResult?.Invoke(result);
    }

    public bool LoadData()
    {
        if(PlayerPrefs.GetInt(SavePresentKey) == 1)
        {
            LoadedData = new LoadedData();
            LoadedData.PlayerHealth = PlayerPrefs.GetInt(PlayerHealthKey);
            LoadedData.SceneIndex = PlayerPrefs.GetInt(SceneKey);
            return true;
        }

        return false;
    }

    public void ResetData()
    {
        //Could use PlayerPrefs.DeleteAll() instead to remove 2 lines of code here. Just a thought
        PlayerPrefs.DeleteKey(PlayerHealthKey);
        PlayerPrefs.DeleteKey(SavePresentKey);
        PlayerPrefs.DeleteKey(SceneKey);
        LoadedData = null;
    }

    public void SaveData(int sceneIndex , int playerHealth)
    {
        if(LoadedData == null)
        {
            LoadedData = new LoadedData();
        }

        LoadedData.PlayerHealth = playerHealth;
        LoadedData.SceneIndex = sceneIndex;
        
        PlayerPrefs.SetInt(PlayerHealthKey , playerHealth);
        PlayerPrefs.SetInt(SavePresentKey , 1);
        PlayerPrefs.SetInt(SceneKey , sceneIndex);
    }

    #endregion
}

public class LoadedData
{
    public int PlayerHealth = -1;
    public int SceneIndex = -1;
}