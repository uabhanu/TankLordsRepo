using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMechanism : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameObject coinsScorePanelObj;
    [SerializeField] private GameObject healthPanelObj;
    [SerializeField] private GameObject highScorePanelObj;
    [SerializeField] private GameObject pauseButtonPanelObj;
    [SerializeField] private GameObject pauseMenuPanelObj;
    
    #endregion

    #region Functions
    
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseButton()
    {
        coinsScorePanelObj.SetActive(false);
        healthPanelObj.SetActive(false);
        highScorePanelObj.SetActive(false);
        pauseButtonPanelObj.SetActive(false);
        pauseMenuPanelObj.SetActive(true);
        highScorePanelObj.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeButton()
    {
        coinsScorePanelObj.SetActive(true);
        healthPanelObj.SetActive(true);
        highScorePanelObj.SetActive(true);
        pauseButtonPanelObj.SetActive(true);
        pauseMenuPanelObj.SetActive(false);
        highScorePanelObj.SetActive(true);
        Time.timeScale = 1;
    }
    
    #endregion
}
