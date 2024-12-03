using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonFunctions : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype0");
        Debug.Log("Start");
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
