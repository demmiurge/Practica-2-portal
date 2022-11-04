using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovementWithRigidbody : MonoBehaviour
{
    private Vector3 m_PlayerMovementInput;
    private Vector2 m_PlayerMouseInput;
    private float m_Rot;

    [SerializeField] private LayerMask m_FloorMask;
    [SerializeField] private Transform m_FeetTransform;
    [SerializeField] private Transform m_PlayerCamera;
    [SerializeField] private Rigidbody m_PlayerRigidbody;
    [Space] 
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Sensitivity;
    [SerializeField] private float m_JumpForce;

    Vector3 l_MoveVector;
    public Vector3 g_Movement => l_MoveVector;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        m_PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        m_PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        l_MoveVector = transform.TransformDirection(m_PlayerMovementInput) * m_Speed;
        m_PlayerRigidbody.velocity = new Vector3(l_MoveVector.x, m_PlayerRigidbody.velocity.y, l_MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
            if (Physics.CheckSphere(m_FeetTransform.position, 0.1f, m_FloorMask))
                m_PlayerRigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
    }

    private void MovePlayerCamera()
    {
        m_Rot -= m_PlayerMouseInput.y * m_Sensitivity;

        transform.Rotate(0.0f, m_PlayerMouseInput.x * m_Sensitivity, 0.0f);
        m_PlayerCamera.transform.localRotation = Quaternion.Euler(m_Rot, 0f, 0f);
    }
}
