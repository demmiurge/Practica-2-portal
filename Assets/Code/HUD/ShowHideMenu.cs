using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMenu : MonoBehaviour
{
    public KeyCode m_PauseMenuKeyCode = KeyCode.P;
    public GameObject m_PauseMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        m_PauseMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_PauseMenuKeyCode))
            HideShowCanvas(!m_PauseMenuCanvas.activeSelf);
    }

    public void HideShowCanvas(bool l_Status)
    {
        m_PauseMenuCanvas.SetActive(l_Status);
        if (l_Status)
        {
            ShowMouse();
            PauseGame();
        }
        else
        {
            HideMouse();
            ResumeGame();
        }
    }

    void ShowMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void HideMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
