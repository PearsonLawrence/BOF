using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    public GameObject ControlsMenu;
    public GameObject PauseHome;
    public string CurrentScene;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene(CurrentScene);
    }
    public void Controls()
    {
        ControlsMenu.SetActive(true);
        PauseHome.SetActive(false);
    }
    public void BackControls()
    {
        ControlsMenu.SetActive(false);
        PauseHome.SetActive(true);
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title Screen");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
