using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");     // load scene "Game" when pressed play
    }

    public void Exit()
    {
        Application.Quit();                 // quit game when pressed exit
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;     // activate or deactivate fullscreen
    }
}
