using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPlayerPositionAttribute : GameObjectStateLoadReload
{
    Vector3 m_Position;
    Vector3 m_Velocity;


    void Start()
    {
        SetCurrentAttributesAsDefault();
    }

    public override void SetCurrentAttributesAsDefault()
    {
        m_Position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (GetComponent<Rigidbody>() != null)
        {
            Rigidbody l_Rigidbody = GetComponent<Rigidbody>();
            m_Velocity = l_Rigidbody.velocity;
        }
    }

    public override void LoadDefaultAttributes()
    {
        ChangeFlagOnCharacterController(false);

        transform.position = m_Position;

        if (GetComponent<Rigidbody>() != null)
        {
            Rigidbody l_Rigidbody = GetComponent<Rigidbody>();
            l_Rigidbody.velocity = m_Velocity;
        }

        ChangeFlagOnCharacterController(true);
    }

    void ChangeFlagOnCharacterController(bool l_Active)
    {
        if (GetComponent<CharacterController>() != null)
            GetComponent<CharacterController>().enabled = l_Active;
    }
}
