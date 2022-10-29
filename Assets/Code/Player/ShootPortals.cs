using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortals : MonoBehaviour
{
    public Portal m_BluePortal;
    public Portal m_OrangePortal;
    public Portal m_Dummie;

    public float m_MaxShootDistance = 50.0f;
    public LayerMask m_ShootingLayerMask;

    public Camera m_PlayerCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_BluePortal.gameObject.SetActive(false);
        m_OrangePortal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot(m_BluePortal);
        if (Input.GetMouseButtonDown(1))
            Shoot(m_OrangePortal);
    }

    void Shoot(Portal l_Portal)
    {
        Vector3 l_Position;
        Vector3 l_Normal;

        if (l_Portal.IsValidPosition(m_PlayerCamera.transform.position, m_PlayerCamera.transform.forward, m_MaxShootDistance, m_ShootingLayerMask, out l_Position, out l_Normal))
            l_Portal.gameObject.SetActive(true);
        else
            l_Portal.gameObject.SetActive(false);
    }
}
