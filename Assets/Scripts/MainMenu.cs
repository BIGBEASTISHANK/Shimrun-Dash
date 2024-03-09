using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private GameObject startMenu, controllMenu;

    // Go to controlls
    public void ControllMenu()
    {
        startMenu.SetActive(false);
        controllMenu.SetActive(true);
    }

    // Go back to main menu
    public void StartMenu()
    {
        startMenu.SetActive(true);
        controllMenu.SetActive(false);
    }

    // Quit game
    public void Quit() { Application.Quit(); }

    // Start Game
    public void Play() { SceneManager.LoadScene("Scenes/Levels/Level 1"); }
}   