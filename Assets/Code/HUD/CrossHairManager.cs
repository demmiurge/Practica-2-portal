using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairManager : MonoBehaviour
{
    public Image m_Empty;
    public Image m_Blue;
    public Image m_Orange;
    public Image m_Full;

    [Space(10)]
    public Portal m_BluePortal;
    public Portal m_OrangePortal;

    // Start is called before the first frame update
    void Start()
    {
        m_Empty.gameObject.SetActive(true);
        m_Blue.gameObject.SetActive(false);
        m_Orange.gameObject.SetActive(false);
        m_Full.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_BluePortal.gameObject.activeSelf && m_OrangePortal.gameObject.activeSelf == false)
        {
            m_Blue.gameObject.SetActive(true);
            m_Empty.gameObject.SetActive(false);
            m_Full.gameObject.SetActive(false);
        }
        else if (m_OrangePortal.gameObject.activeSelf && m_BluePortal.gameObject.activeSelf == false)
        {
            m_Orange.gameObject.SetActive(true );
            m_Empty.gameObject.SetActive(false);
            m_Full.gameObject.SetActive(false);
        }
        else if(m_BluePortal.gameObject.activeSelf && m_OrangePortal.gameObject.activeSelf)
        {
            m_Full.gameObject.SetActive(true);
            m_Blue.gameObject.SetActive(false);
            m_Empty.gameObject.SetActive(false);
            m_Orange.gameObject.SetActive(false);
        }
        else
        {
            m_Empty.gameObject.SetActive(true);
            m_Blue.gameObject.SetActive(false);
            m_Full.gameObject.SetActive(false);
            m_Orange.gameObject.SetActive(false);
        }
    }
}
