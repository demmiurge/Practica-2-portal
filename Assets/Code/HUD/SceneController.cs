using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneController : MonoBehaviour
{
    public void GoToScene(string l_SceneName)
    {
        SceneManager.LoadScene(l_SceneName);
    }

    public void ExitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }
}
