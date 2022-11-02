using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        m_PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();

    }

    private void MovePlayer()
    {
        Vector3 l_MoveVector = transform.TransformDirection(m_PlayerMovementInput) * m_Speed;
        m_PlayerRigidbody.velocity = new Vector3(l_MoveVector.x, m_PlayerRigidbody.velocity.y, l_MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
            //if (Physics.CheckSphere(m_FeetTransform.position, 0.1f))
                
    }

    private void MovePlayerCamera()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
