using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
}
