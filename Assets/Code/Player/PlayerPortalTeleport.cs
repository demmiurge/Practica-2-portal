using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalTeleport : MonoBehaviour
{
    public CharacterController m_CharacterController;
    [Range(30.0f, 90.0f)] public float m_AngleDegrees;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            Portal l_Portal = other.GetComponent<Portal>();
            if (Vector3.Dot(l_Portal.transform.forward, -GetComponent<PlayerMovement>()._Direction) > Mathf.Cos(m_AngleDegrees * Mathf.Deg2Rad))
            {
                Teleport(l_Portal);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }

    private void Teleport(Portal _Portal)
    {
        Rigidbody l_Rigidbody = GetComponent<Rigidbody>();

        Vector3 l_LocalPosition = _Portal.m_OtherPortalTransform.InverseTransformPoint(transform.position);
        Vector3 l_LocalDirection = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(transform.forward);
        Vector3 l_LocalDirectionMovement = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(GetComponent<PlayerMovement>()._Direction);
        Vector3 l_WorldDirectionMovement = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirectionMovement);

        //Vector3 l_LocalVelocity = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(l_Rigidbody.velocity);
        //Vector3 l_WorldVelocity = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalVelocity);

        l_Rigidbody.isKinematic = true;

        //Vector3 l_WorldVeloctyNormalized = l_WorldVelocity.normalized;
        Vector3 l_WorldDirectionNormalized = l_WorldDirectionMovement.normalized;

        m_CharacterController.enabled = false;

        transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirection);
        GetComponent<PlayerCameraMovement>().m_Yaw = transform.rotation.eulerAngles.y;
        transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(l_LocalPosition) + l_WorldDirectionNormalized * 1.5f;
        l_Rigidbody.isKinematic = false;
        m_CharacterController.enabled = true;

        Debug.Break();
    }
}
