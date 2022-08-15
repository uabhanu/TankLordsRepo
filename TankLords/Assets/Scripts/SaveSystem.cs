using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    #region Variables
    
    private bool _isInitialized;
    
    [SerializeField] private string playerHealthKey = "PlayerHealth";
    [SerializeField] private string savePresentKey = "SavePresent";
    [SerializeField] private string sceneKey = "SceneIndex";
    [SerializeField] private UnityEvent<bool> onDataLoadedResult;
    
    public LoadedData LoadedData
    {
        get;
        private set;
    }

    #endregion
    
    #region Functions

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var result = LoadData();
        onDataLoadedResult?.Invoke(result);
    }

    public bool LoadData()
    {
        if(PlayerPrefs.GetInt(savePresentKey) == 1)
        {
            LoadedData = new LoadedData();
            LoadedData.PlayerHealth = PlayerPrefs.GetInt(playerHealthKey);
            LoadedData.SceneIndex = PlayerPrefs.GetInt(sceneKey);
            return true;
        }

        return false;
    }

    public void ResetData()
    {
        //Could use PlayerPrefs.DeleteAll() instead to remove 2 lines of code here. Just a thought
        PlayerPrefs.DeleteKey(playerHealthKey);
        PlayerPrefs.DeleteKey(savePresentKey);
        PlayerPrefs.DeleteKey(sceneKey);
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
        
        PlayerPrefs.SetInt(playerHealthKey , playerHealth);
        PlayerPrefs.SetInt(savePresentKey , 1);
        PlayerPrefs.SetInt(sceneKey , sceneIndex);
    }

    #endregion
}

public class LoadedData
{
    public int PlayerHealth = -1;
    public int SceneIndex = -1;
}