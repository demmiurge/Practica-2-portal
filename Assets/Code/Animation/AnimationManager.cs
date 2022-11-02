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
        m_ElementAnimation.CrossFade(m_OnElementAnimation.name);
    }

    public void PlayOffAnimation()
    {
        m_ElementAnimation.CrossFade(m_OffElementAnimation.name);
    }
}
