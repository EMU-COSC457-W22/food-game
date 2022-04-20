using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartLevelOne()
    {
<<<<<<< HEAD:Food_Freedom_Frenzy/Assets/Scripts/Menu/GameOver.cs
        SceneManager.LoadScene("Level_1");
=======
        // SceneManager.LoadScene("Release_2");
        SceneManager.LoadScene("Release_2_COPY");
>>>>>>> 9474859ed1ea129da68ab71e38a3a6ea9edcac1b:Food_Freedom_Frenzy/Assets/Scripts/GameOver.cs
    }

    public void RestartLevelTwo()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void RestartLevelThree()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
}
