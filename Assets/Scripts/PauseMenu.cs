using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPauseMenu, pauseMenu, controllMenu;

    public void Resume()
    {
        Time.timeScale = 1;
        mainPauseMenu.SetActive(false);
    }

    public void Controlls()
    {
        pauseMenu.SetActive(false);
        controllMenu.SetActive(true);
    }

    public void ControllsGoBack()
    {
        controllMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/Menus/Main Menu");
    }
}