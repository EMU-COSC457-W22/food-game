using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    public string currentLevel = "";

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            currentLevel = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void NextLevel()
    {

    }
}
