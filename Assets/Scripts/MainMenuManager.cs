using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string firstScene;

    public void BeginGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
