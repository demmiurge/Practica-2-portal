using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LittleButton : MonoBehaviour
{
    private bool m_CanPressButton;

    public KeyCode m_InteractiveKeyCode = KeyCode.E;

    public GameObject m_UXInteractive;

    public UnityEvent m_ButtonPressed;

    void Update()
    {
        if (m_CanPressButton && Input.GetKeyDown(m_InteractiveKeyCode))
        {
            Debug.Log("PULSO");
            m_ButtonPressed.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanPressButton = true;
            m_UXInteractive.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanPressButton = false;
            m_UXInteractive.SetActive(false);
        }
    }
}
