using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_OffsetTeleportPortal = 1.5f;
    Portal m_ExitPortal = null;
    [Range(30.0f, 90.0f)] public float m_AngleDegrees;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            Portal l_Portal = other.GetComponent<Portal>();

            if (Vector3.Dot(l_Portal.transform.forward, -GetComponent<PlayerMovementWithRigidbody>().g_Movement) > Mathf.Cos(m_AngleDegrees * Mathf.Deg2Rad))
                Teleport(other.GetComponent<Portal>());
        }
    }

    private void Teleport(Portal _Portal)
    {
        Vector3 l_LocalPosition = _Portal.m_OtherPortalTransform.InverseTransformPoint(transform.position);
        Vector3 l_LocalDirection = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(transform.forward);

        Vector3 l_LocalDirectionMovement = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(m_Rigidbody.velocity);
        Vector3 l_WorldDirectionMovement = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirectionMovement);

        m_Rigidbody.isKinematic = true;
        transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirection);
        Vector3 l_WorldDirectionNormalized = l_WorldDirectionMovement.normalized;

        transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(l_LocalPosition) + l_WorldDirectionNormalized * 1.5f;
        transform.localScale *= (_Portal.m_MirrorPortal.transform.localScale.x / _Portal.transform.localScale.x);
        m_Rigidbody.isKinematic = false;
        m_ExitPortal = _Portal.m_MirrorPortal;
    }
}
