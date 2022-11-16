using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPortal : MonoBehaviour
{
    public Camera m_PlayerCamera;
    public float m_OffsetNearPlane;

    public List<Transform> m_ValidPoints;

    public float m_MinValidDistance = 0.2f;
    public float m_MaxValidDistance = 1.5f;
    public float m_MinDotValidAngle = 0.995f;


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

    // Update is called once per frame
    void Update()
    {
        
    }
}
