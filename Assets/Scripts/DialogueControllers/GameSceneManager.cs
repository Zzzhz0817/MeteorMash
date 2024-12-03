using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().GetVariable("game_end").ToString() == "true")
        {
            Debug.Log("Game Ended");
            SceneManager.LoadScene("Menu-Finish-Game");
        }
    }
}
