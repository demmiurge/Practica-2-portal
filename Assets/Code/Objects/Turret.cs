using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    public LineRenderer m_Laser;
    public LayerMask m_LaserLayerMask;
    public float m_MaxLaserDistance = 250.0f;
    public float m_AngleDegrees = 45.0f;
    public UnityEvent m_KillPlayerEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool l_LaserAlive = Vector3.Dot(m_Laser.transform.up, Vector3.up) > Mathf.Cos(m_AngleDegrees * Mathf.Deg2Rad);
        m_Laser.gameObject.SetActive(l_LaserAlive);
        if (l_LaserAlive)
        {
            Ray l_Ray = new Ray(m_Laser.transform.position, m_Laser.transform.forward);
            float l_LaserDistance = m_MaxLaserDistance;
            RaycastHit l_RaycastHit;
            if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxLaserDistance, m_LaserLayerMask.value))
            {
                l_LaserDistance = Vector3.Distance(m_Laser.transform.position, l_RaycastHit.point);
                if (l_RaycastHit.collider.tag == "RefractionCube")
                {
                    l_RaycastHit.collider.GetComponent<RefractionCube>().CreateRefraction();
                }

                if (l_RaycastHit.collider.tag == "Portal")
                {
                    //l_RaycastHit.collider.GetComponent<Portal>().m_MirrorPortal.CreateRefraction();
                    l_RaycastHit.collider.GetComponent<Portal>().m_MirrorPortal.CreateRefractionNew(l_RaycastHit,l_Ray, l_RaycastHit.collider.GetComponent<Portal>());
                    //Physics.IgnoreCollision(m_Laser.GetComponent<Collider>(), l_RaycastHit.collider);
                }

                if(l_RaycastHit.collider.tag == "Player")
                {
                    m_KillPlayerEvent.Invoke();
                }
            }

            m_Laser.SetPosition(1, new Vector3(0.0f, 0.0f, l_LaserDistance));
        }
    }
}
