using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    // Going back to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/Menus/Main Menu");
    }

    // Quiting game
    public void Quit() { Application.Quit(); }
}