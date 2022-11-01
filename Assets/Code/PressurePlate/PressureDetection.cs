using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressureDetection : MonoBehaviour
{
    public Animation m_ButtonAnimation;
    public AnimationClip m_PressButtonAnimation;
    public AnimationClip m_ReleaseButtonAnimation;

    public float m_NecessaryMass = 0.5f;

    private int m_NumberOfElementsAbove;

    public UnityEvent m_Activation;
    public UnityEvent m_Deactivation;

    // Start is called before the first frame update
    void Start()
    {
        m_NumberOfElementsAbove = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Rigidbody>()) return;
        Rigidbody l_Rigidbody = other.GetComponent<Rigidbody>();

        if (m_NecessaryMass < l_Rigidbody.mass)
            m_NumberOfElementsAbove++;

        if (m_NumberOfElementsAbove == 1)
        {
            StartCoroutine(SetActive());
        }
    }

    IEnumerator SetActive()
    {
        m_ButtonAnimation.Play(m_PressButtonAnimation.name);
        yield return new WaitForSeconds(m_PressButtonAnimation.length);
        m_Activation.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Rigidbody>()) return;
        Rigidbody l_Rigidbody = other.GetComponent<Rigidbody>();

        if (m_NecessaryMass < l_Rigidbody.mass)
            m_NumberOfElementsAbove--;

        if (m_NumberOfElementsAbove == 0)
            SetDisabled();
    }

    void SetDisabled()
    {
        m_Deactivation.Invoke();
        m_ButtonAnimation.Play(m_ReleaseButtonAnimation.name);
    }
}
