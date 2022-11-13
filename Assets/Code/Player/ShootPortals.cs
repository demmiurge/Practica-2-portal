using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortals : MonoBehaviour
{
    [Header("Portals")]
    public Portal m_BluePortal;
    public Portal m_OrangePortal;
    public Portal m_Dummie;

    public float m_MaxShootDistance = 50.0f;
    public LayerMask m_ShootingLayerMask;
    public Transform m_PitchController;

    [Header("Objects")]
    public Transform m_AttachingPosition;
    Rigidbody m_ObjectAttached;
    bool m_AttachedObject = false;
    public float m_AttachingObjectSpeed = 3.0f;
    Quaternion m_AttachingObjectStartRotation;
    public float m_MaxAttachDistance;
    public LayerMask m_AttachObjectLayermask;
    public float m_AttachedObjectThrowForce;
    public float m_AttachedObjectReleaseForce;
    public KeyCode m_AttachObjectKeyCode = KeyCode.E;

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
        /* if (Input.GetMouseButtonDown(0))
             Shoot(m_BluePortal);
         if (Input.GetMouseButtonDown(1))
             Shoot(m_OrangePortal);*/

        if (Input.GetKeyDown(m_AttachObjectKeyCode) && CanAttach())
        {
            AttachObject();
        }

        if (m_ObjectAttached && !m_AttachedObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ThrowAttachedObject(m_AttachedObjectThrowForce);
                m_ObjectAttached = null;
            }
            if (Input.GetMouseButtonDown(1))
            {
                ThrowAttachedObject(m_AttachedObjectReleaseForce);
                m_ObjectAttached = null;
            }
        }
        else if (!m_AttachedObject)
        {
            if (Input.GetMouseButtonDown(0))
                Shoot(m_BluePortal);
            if (Input.GetMouseButtonDown(1))
                Shoot(m_OrangePortal);

        }

        if (m_AttachedObject)
        {
            UpdateAttachedObject();
        }
    }

    bool CanAttach()
    {
        return m_ObjectAttached == null;
    }

    void AttachObject()
    {
        Ray l_Ray = m_PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxAttachDistance, m_AttachObjectLayermask))
        {
            if (l_RaycastHit.collider.tag == "Cube" || l_RaycastHit.collider.tag == "Turret" || l_RaycastHit.collider.tag == "RefractionCube")
            {
                m_AttachedObject = true;
                m_ObjectAttached = l_RaycastHit.collider.GetComponent<Rigidbody>();
                m_ObjectAttached.gameObject.layer = LayerMask.NameToLayer("Weapon");
                foreach(Transform child in m_ObjectAttached.transform)
                    child.gameObject.layer = LayerMask.NameToLayer("Weapon");
                m_ObjectAttached.GetComponent<Companion>().SetAttached(true);
                m_ObjectAttached.isKinematic = true;
                m_AttachingObjectStartRotation = l_RaycastHit.collider.transform.rotation;
            }
        }
    }

    void ThrowAttachedObject(float force)
    {
        if (m_ObjectAttached != null)
        {
            m_AttachedObject = false;
            m_ObjectAttached.transform.SetParent(null);
            m_ObjectAttached.gameObject.layer = LayerMask.NameToLayer("Objects");
             foreach(Transform child in m_ObjectAttached.transform)
                    child.gameObject.layer = LayerMask.NameToLayer("Objects");
            m_ObjectAttached.GetComponent<Companion>().SetAttached(false);
            m_ObjectAttached.isKinematic = false;
            m_ObjectAttached.AddForce(m_PitchController.forward * force);
        }
    }

    void UpdateAttachedObject()
    {
        Vector3 l_EulerAngles = m_AttachingPosition.rotation.eulerAngles;
        Vector3 l_Direction = m_AttachingPosition.transform.position - m_ObjectAttached.transform.position;
        float l_Distance = l_Direction.magnitude;
        float l_Movement = m_AttachingObjectSpeed * Time.deltaTime;

        if (l_Movement >= l_Distance)
        {
            m_AttachedObject = false;
            m_ObjectAttached.transform.SetParent(m_AttachingPosition);
            m_ObjectAttached.transform.localPosition = Vector3.zero;
            m_ObjectAttached.transform.localRotation = Quaternion.identity;
        }
        else
        {
            l_Direction /= l_Distance;
            m_ObjectAttached.MovePosition(m_ObjectAttached.transform.position + l_Direction * l_Movement);
            m_ObjectAttached.MoveRotation(Quaternion.Lerp(m_AttachingObjectStartRotation, Quaternion.Euler(0.0f, l_EulerAngles.y, l_EulerAngles.z), 1.0f - Mathf.Min(l_Distance / 1.5f, 1.0f)));
        }
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
