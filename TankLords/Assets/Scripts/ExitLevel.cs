using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    #region Variables
    
    private GameManager _gameManager;

    public LayerMask PlayerMask;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if(((1 << col2D.gameObject.layer) & PlayerMask) != 0)
        {
            _gameManager.SaveData();
            _gameManager.LoadNextLevel();
        }
    }
    
    #endregion
}
