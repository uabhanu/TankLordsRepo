using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMechanism : MonoBehaviour
{
    [SerializeField] private GameObject healthPanelObj;
    [SerializeField] private GameObject pauseButtonPanelObj;
    [SerializeField] private GameObject pauseMenuPanelObj;
    [SerializeField] private GameObject scorePanelObj;

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void PauseButton()
    {
        healthPanelObj.SetActive(false);
        pauseButtonPanelObj.SetActive(false);
        pauseMenuPanelObj.SetActive(true);
        scorePanelObj.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeButton()
    {
        healthPanelObj.SetActive(true);
        pauseButtonPanelObj.SetActive(true);
        pauseMenuPanelObj.SetActive(false);
        scorePanelObj.SetActive(true);
        Time.timeScale = 1;
    }
}
