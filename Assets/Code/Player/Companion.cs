using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    private bool isAttached = false;
    Rigidbody m_Rigidbody;
    public float m_OffsetTeleportPortal = 1.5f;
    Portal m_ExitPortal = null;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void SetAttached(bool Attached)
    {
        isAttached = Attached;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal" && isAttached == false)
        {
            Portal l_Portal = other.GetComponent<Portal>();

            if (l_Portal != m_ExitPortal)
            {
                Teleport(l_Portal);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Portal")
        {
            if (other.GetComponent<Portal>() == m_ExitPortal)
            {
                m_ExitPortal = null;
            }
        }
    }

    private void Teleport(Portal _Portal)
    {
        Vector3 l_LocalPosition = _Portal.m_OtherPortalTransform.InverseTransformPoint(transform.position);
        Vector3 l_LocalDirection = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(transform.forward);

        Vector3 l_LocalVelocity = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(m_Rigidbody.velocity);

        Vector3 l_WorldVelocity = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalVelocity);

        m_Rigidbody.isKinematic = true;

        transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirection);

        Vector3 l_WorldVeloctyNormalized = l_WorldVelocity.normalized;

        transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(l_LocalPosition) + l_WorldVeloctyNormalized * 1.5f;
        transform.localScale *= (_Portal.m_MirrorPortal.transform.localScale.x / _Portal.transform.localScale.x);
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.velocity = l_WorldVelocity;
        m_ExitPortal = _Portal.m_MirrorPortal;
    }
}
