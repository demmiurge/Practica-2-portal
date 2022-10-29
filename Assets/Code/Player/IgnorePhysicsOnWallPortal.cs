using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePhysicsOnWallPortal : MonoBehaviour
{
    public Collider m_Collider;
    public string m_TagPortal;
    public string m_TagWall;

    private bool m_ContactPortal = false;
    private Collider m_Wall;

    void OnTriggerEnter(Collider other)
    {
        if (m_TagPortal == other.tag)
            m_ContactPortal = true;

        if (m_ContactPortal)
            if (m_TagWall == other.tag)
                Physics.IgnoreCollision(m_Collider, other.GetComponent<Collider>());
    }

    void OnTriggerExit(Collider other)
    {
        if (m_TagPortal == other.tag)
            m_ContactPortal = false;

        if (m_TagWall == other.tag)
            Physics.IgnoreCollision(m_Collider, other.GetComponent<Collider>(), false);
    }
}
