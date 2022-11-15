using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFake : MonoBehaviour
{
    public Camera m_Camera;
    public Transform m_OtherPortalTransform;
    public PortalFake m_MirrorPortal;
    public Camera m_PlayerCamera;
    public float m_OffsetNearPlane;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        Vector3 l_WorldPosition = m_PlayerCamera.transform.position;
        Vector3 l_LocalPosition = m_OtherPortalTransform.InverseTransformPoint(l_WorldPosition);
        m_MirrorPortal.m_Camera.transform.position = m_MirrorPortal.transform.TransformPoint(l_LocalPosition);

        Vector3 l_WorldDirection = m_PlayerCamera.transform.forward;
        Vector3 l_LocalDirection = m_OtherPortalTransform.InverseTransformDirection(l_WorldDirection);
        m_MirrorPortal.m_Camera.transform.forward = m_MirrorPortal.transform.TransformDirection(l_LocalDirection);

        float l_Distance = Vector3.Distance(m_MirrorPortal.m_Camera.transform.position, m_MirrorPortal.transform.position);
        m_MirrorPortal.m_Camera.nearClipPlane = l_Distance + m_OffsetNearPlane;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(true);
    }
}
