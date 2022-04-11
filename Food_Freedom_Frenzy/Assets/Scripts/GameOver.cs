using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartLevelOne()
    {
        SceneManager.LoadScene("Release_2");
    }

    public void RestartLevelTwo()
    {
        SceneManager.LoadScene("Release_2_pt2");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Ended");
    }
}
