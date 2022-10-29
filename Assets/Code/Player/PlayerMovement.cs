using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float m_Yaw;
    float m_Pitch;

    public float m_YawRotationalSpeed = 720;
    public float m_PitchRotationalSpeed = 720;

    public float m_MinPitch = -75;
    public float m_MaxPitch = 45;

    public Transform m_PitchController;

    public bool m_UseYawInverted;
    public bool m_UsePitchInverted = true;

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

    // Start is called before the first frame update
    void Start()
    {
        m_Yaw = transform.rotation.y;
        m_Pitch = m_PitchController.localRotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 l_RightDirection = transform.right;
        Vector3 l_ForwardDirection = transform.forward;
        Vector3 l_Direction = Vector3.zero;
        float l_Speed = m_Speed;

        float l_MouseX = Input.GetAxis("Mouse X");
        float l_MouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKey(m_UpKeyCode))
            l_Direction = l_ForwardDirection;

        if (Input.GetKey(m_DownKeyCode))
            l_Direction = -l_ForwardDirection;

        if (Input.GetKey(m_RightKeyCode))
            l_Direction += l_RightDirection;

        if (Input.GetKey(m_LeftKeyCode))
            l_Direction -= l_RightDirection;

        if (Input.GetKeyDown(m_JumpKeyCode) && m_OnGround)
            m_VerticalSpeed = m_JumpSpeed;

        float l_FOV = m_NormalMovementFOV;

        if (Input.GetKey(m_RunKeyCode))
        {
            l_Speed = m_Speed * m_FastSpeedMultiplier;
            l_FOV = m_RunMovementFOV;
        }

        m_PlayerCamera.fieldOfView = Mathf.Lerp(m_PlayerCamera.fieldOfView, l_FOV, 0.1f);

        l_Direction.Normalize();

        // Rotation
        m_Yaw = m_Yaw + l_MouseX * m_YawRotationalSpeed * Time.deltaTime * (m_UseYawInverted ? -1.0f : 1.0f);
        m_Pitch = m_Pitch + l_MouseY * m_PitchRotationalSpeed * Time.deltaTime * (m_UsePitchInverted ? -1.0f : 1.0f);
        m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);

        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);

        Vector3 l_Movement = l_Direction * l_Speed * Time.deltaTime;

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
}
