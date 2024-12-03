using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonFunctions : MonoBehaviour
{
    public GameObject player;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Menu-Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
