using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [HideInInspector]
    public float m_Yaw;
    float m_Pitch;

    public float m_YawRotationalSpeed = 720;
    public float m_PitchRotationalSpeed = 720;

    public float m_MinPitch = -75;
    public float m_MaxPitch = 45;

    public Transform m_PitchController;

    public bool m_UseYawInverted;
    public bool m_UsePitchInverted = true;


    // Start is called before the first frame update
    void Start()
    {
        m_Yaw = transform.rotation.y;
        m_Pitch = m_PitchController.localRotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        float l_MouseX = Input.GetAxis("Mouse X");
        float l_MouseY = Input.GetAxis("Mouse Y");

        m_Yaw += l_MouseX * m_YawRotationalSpeed * Time.deltaTime * (m_UseYawInverted ? -1.0f : 1.0f);
        m_Pitch += l_MouseY * m_PitchRotationalSpeed * Time.deltaTime * (m_UsePitchInverted ? -1.0f : 1.0f);
        m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);

        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);
    }
}
