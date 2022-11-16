using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiePlayer : MonoBehaviour
{
    public UnityEvent m_DeathZoneTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone" || other.tag == "DeadZoneLaser")
        {
            Debug.Log("La toque");
            m_DeathZoneTouched.Invoke();
        }
    }
}
