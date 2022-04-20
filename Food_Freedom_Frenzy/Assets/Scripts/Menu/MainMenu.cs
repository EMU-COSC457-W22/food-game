using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
<<<<<<< HEAD:Food_Freedom_Frenzy/Assets/Scripts/Menu/MainMenu.cs
        SceneManager.LoadScene("Level_1");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
=======
        // SceneManager.LoadScene("Release_2");
        SceneManager.LoadScene("Release_2_COPY");
>>>>>>> 9474859ed1ea129da68ab71e38a3a6ea9edcac1b:Food_Freedom_Frenzy/Assets/Scripts/MainMenu.cs
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
}
