using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Release_2");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
}
