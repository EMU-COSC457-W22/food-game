using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartLevelOne()
    {
        SceneManager.LoadScene("Level_1");
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
