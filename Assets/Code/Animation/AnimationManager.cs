using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animation m_ElementAnimation;
    public AnimationClip m_OnElementAnimation;
    public AnimationClip m_OffElementAnimation;

    private float m_Time;

    // Start is called before the first frame update
    void Start()
    {
        foreach (AnimationState state in m_ElementAnimation)
        {
            state.speed = 0.5F;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ElementAnimation.isPlaying)
        {
            Debug.Log("MORE TIME");
            m_Time += Time.deltaTime;
        }
            
    }

    public void PlayOnAnimation()
    {
        float l_PointToStart = m_OnElementAnimation.averageDuration - m_Time;
        m_ElementAnimation.CrossFade(m_OnElementAnimation.name);
        m_Time = 0.0f;
    }

    public void PlayOffAnimation()
    {
        float l_PointToStart = m_OnElementAnimation.averageDuration;
        m_ElementAnimation.Blend(m_OffElementAnimation.name, l_PointToStart);

        m_Time = 0.0f;
    }
}
