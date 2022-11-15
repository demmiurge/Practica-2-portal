using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleLaserScreen : MonoBehaviour
{
    public GameObject m_GateLaserScreenUpOne;
    public GameObject m_GateLaserScreenDownOne;

    public GameObject m_GateLaserScreenUpTwo;
    public GameObject m_GateLaserScreenDownTwo;

    public bool m_ActiveFront = true;

    // Start is called before the first frame update
    void Start()
    {
        ChangeStatus();
    }

    public void ChangeLaserScreen()
    {
        m_ActiveFront = !m_ActiveFront;
        ChangeStatus();
    }

    void ChangeStatus()
    {
        m_GateLaserScreenUpOne.SetActive(m_ActiveFront);
        m_GateLaserScreenDownOne.SetActive(m_ActiveFront);

        m_GateLaserScreenUpTwo.SetActive(!m_ActiveFront);
        m_GateLaserScreenDownTwo.SetActive(!m_ActiveFront);
    }
}
