using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Portal : MonoBehaviour
{
    public Camera m_Camera;
    public Transform m_OtherPortalTransform;
    public Portal m_MirrorPortal;
    public Camera m_PlayerCamera;
    public float m_OffsetNearPlane;

    public List<Transform> m_ValidPoints;

    public float m_MinValidDistance = 0.2f;
    public float m_MaxValidDistance = 1.5f;
    public float m_MinDotValidAngle = 0.995f;

    public LineRenderer m_LineRenderer;
    public float m_MaxDistance = 250.0f;
    public LayerMask m_CollisionLayerMask;
    bool m_CreateRefraction;

    public UnityEvent m_KillPlayerEvent;

    float m_MaxLaserDistance = 250.0f;

    void Update()
    {
        m_LineRenderer.gameObject.SetActive(m_CreateRefraction);
        m_CreateRefraction = false;
    }

    /*public void CreateRefraction()
    {
        m_CreateRefraction = true;
        Vector3 l_EndRaycastPosition = Vector3.forward * m_MaxDistance;

        RaycastHit l_RaycastHit;
        if (Physics.Raycast(new Ray(m_LineRenderer.transform.position, m_LineRenderer.transform.forward), out l_RaycastHit, m_MaxDistance, m_CollisionLayerMask.value))
        {
            l_EndRaycastPosition = Vector3.forward * l_RaycastHit.distance;

            if (l_RaycastHit.collider.tag == "Portal")
            {
                //Reflect ray
                l_RaycastHit.collider.GetComponent<Portal>().CreateRefraction();
            }

            if (l_RaycastHit.collider.tag == "Player")
            {
                Debug.Log("You died");
            }
        }

        m_LineRenderer.SetPosition(1, l_EndRaycastPosition);
    }*/

    public void CreateRefractionNew(RaycastHit _Point, Ray _Ray, Portal _Portal)
    {
        m_CreateRefraction = true;
       
        Vector3 l_LocalPosition = _Portal.m_OtherPortalTransform.InverseTransformPoint(_Point.point);
        Vector3 l_LocalDirection = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(_Ray.direction);

        Vector3 l_EndRaycastPosition = l_LocalDirection * m_MaxDistance;

        Vector3 l_LocalDirectionNormalized = l_LocalDirection.normalized;

        m_LineRenderer.transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirectionNormalized);

        m_LineRenderer.transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(new Vector3(l_LocalPosition.x, l_LocalPosition.y, l_LocalPosition.z + 0.5f));
        RaycastHit l_RaycastHitNew;

        if (Physics.Raycast(new Ray(m_LineRenderer.transform.position, m_LineRenderer.transform.forward), out l_RaycastHitNew, m_MaxDistance, m_CollisionLayerMask.value))
        {
            l_EndRaycastPosition = m_LineRenderer.transform.forward * l_RaycastHitNew.distance;

            if (l_RaycastHitNew.collider.tag == "Portal")
            {
                //Reflect ray
                //l_RaycastHitNew.collider.GetComponent<Portal>().CreateRefractionNew(l_RaycastHitNew, _Ray, _Portal);
            }

            if (l_RaycastHitNew.collider.tag == "Player")
            {
                m_KillPlayerEvent.Invoke();
                Debug.Log("dead");
            }
            
        }
        m_LineRenderer.SetPosition(1, l_EndRaycastPosition);
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

    public bool IsValidPosition(Vector3 StartPosition, Vector3 Forward, float MaxDistance, LayerMask PortalLayerMask, out Vector3 Position, out Vector3 Normal)
    {
        Ray l_Ray = new Ray(StartPosition, Forward);
        RaycastHit l_RaycastHit;
        bool l_Valid = false;
        Position = Vector3.zero;
        Normal = Vector3.forward;

        if (Physics.Raycast(l_Ray, out l_RaycastHit, MaxDistance, PortalLayerMask.value))
        {
            if (l_RaycastHit.collider.tag == "DrawableWall")
            {
                l_Valid = true;
                Position = l_RaycastHit.point;
                Normal = l_RaycastHit.normal;
                transform.position = Position;
                transform.rotation = Quaternion.LookRotation(Normal);

                for (int i = 0; i < m_ValidPoints.Count; i++)
                {
                    Vector3 l_Direction = m_ValidPoints[i].position - StartPosition;
                    l_Direction.Normalize();
                    l_Ray = new Ray(StartPosition, l_Direction);
                    if (Physics.Raycast(l_Ray, out l_RaycastHit, MaxDistance, PortalLayerMask.value))
                    {
                        if (l_RaycastHit.collider.tag == "DrawableWall")
                        {
                            float l_Distance = Vector3.Distance(Position, l_RaycastHit.point);
                            //Debug.Log("Distance: " + l_Distance);

                            float l_DotAngle = Vector3.Dot(Normal, l_RaycastHit.normal);
                            if (!(l_Distance >= m_MinValidDistance && l_Distance <= m_MaxValidDistance &&
                                  l_DotAngle > m_MinDotValidAngle))
                                l_Valid = false;
                        }
                        else
                            l_Valid = false;
                    }
                    else
                        l_Valid = false;
                }
            }
        }
        return l_Valid;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
