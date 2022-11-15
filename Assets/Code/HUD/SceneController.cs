using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToScene(string l_SceneName)
    {
        SceneManager.LoadScene(l_SceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
