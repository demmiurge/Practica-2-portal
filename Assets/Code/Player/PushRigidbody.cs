using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigidbody : MonoBehaviour
{
    public float m_PushPower = 2.0f;
    public List<string> m_TagsIgnore;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody l_Rigidbody = hit.collider.attachedRigidbody;

        if (l_Rigidbody == null || l_Rigidbody.isKinematic)
            return;

        foreach (string l_Tag in m_TagsIgnore)
        {
            if (l_Tag == l_Rigidbody.tag)
                return;
        }

        if (hit.moveDirection.y < -0.3)
            return;

        float l_TargetMass = l_Rigidbody.mass;

        Vector3 l_PushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        l_Rigidbody.velocity = l_PushDirection * m_PushPower / l_TargetMass;
    }
}
