using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController m_CharacterController;

    public float m_Speed;
    public float m_FastSpeedMultiplier = 1.5f;

    public KeyCode m_LeftKeyCode = KeyCode.A;
    public KeyCode m_RightKeyCode = KeyCode.D;
    public KeyCode m_UpKeyCode = KeyCode.W;
    public KeyCode m_DownKeyCode = KeyCode.S;
    public KeyCode m_JumpKeyCode = KeyCode.Space;
    public KeyCode m_RunKeyCode = KeyCode.LeftShift;

    float m_VerticalSpeed = 0.0f;
    bool m_OnGround = true;

    public Camera m_PlayerCamera;
    public float m_NormalMovementFOV;
    public float m_RunMovementFOV;

    public float m_JumpSpeed = 10.0f;
    Vector3 m_Direction;
    [Range(30.0f, 90.0f)] public float m_AngleDegrees;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 l_RightDirection = transform.right;
        Vector3 l_ForwardDirection = transform.forward;
        m_Direction = Vector3.zero;
        float l_Speed = m_Speed;

        float l_MouseX = Input.GetAxis("Mouse X");
        float l_MouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKey(m_UpKeyCode))
            m_Direction = l_ForwardDirection;

        if (Input.GetKey(m_DownKeyCode))
            m_Direction = -l_ForwardDirection;

        if (Input.GetKey(m_RightKeyCode))
            m_Direction += l_RightDirection;

        if (Input.GetKey(m_LeftKeyCode))
            m_Direction -= l_RightDirection;

        if (Input.GetKeyDown(m_JumpKeyCode) && m_OnGround)
            m_VerticalSpeed = m_JumpSpeed;

        float l_FOV = m_NormalMovementFOV;

        if (Input.GetKey(m_RunKeyCode))
        {
            l_Speed = m_Speed * m_FastSpeedMultiplier;
            l_FOV = m_RunMovementFOV;
        }

        // m_Camera.fieldOfView = l_FOV;
        m_PlayerCamera.fieldOfView = Mathf.Lerp(m_PlayerCamera.fieldOfView, l_FOV, 0.1f);

        m_Direction.Normalize();

        Vector3 l_Movement = m_Direction * l_Speed * Time.deltaTime;

        m_VerticalSpeed = m_VerticalSpeed + Physics.gravity.y * Time.deltaTime;
        l_Movement.y = m_VerticalSpeed * Time.deltaTime;

        CollisionFlags l_CollisionFlags = m_CharacterController.Move(l_Movement);

        if ((l_CollisionFlags & CollisionFlags.Above) != 0 && m_VerticalSpeed > 0.0f)
            m_VerticalSpeed = 0.0f;
        if ((l_CollisionFlags & CollisionFlags.Below) != 0)
        {
            m_VerticalSpeed = 0.0f;
            m_OnGround = true;
        }
        else
            m_OnGround = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            Portal l_Portal = other.GetComponent<Portal>();
            if (Vector3.Dot(l_Portal.transform.forward, -m_Direction) > Mathf.Cos(m_AngleDegrees * Mathf.Deg2Rad))
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
        Vector3 l_LocalPosition = _Portal.m_OtherPortalTransform.InverseTransformPoint(transform.position);
        Vector3 l_LocalDirection = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(transform.forward);
        Vector3 l_LocalDirectionMovement = _Portal.m_OtherPortalTransform.transform.InverseTransformDirection(m_Direction);
        Vector3 l_WorldDirectionMovement = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirectionMovement);

        m_CharacterController.enabled = false;
        transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirection);
        //m_Yaw = transform.rotation.eulerAngles.y;
        transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(l_LocalPosition) + l_WorldDirectionMovement * 1f;
        m_CharacterController.enabled = true;

        Debug.Break();
    }
}
