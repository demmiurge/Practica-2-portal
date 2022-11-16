using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnElevator : MonoBehaviour
{
    public Animation m_Animation;
    public AnimationClip m_ElevatorAnimation;

    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
        m_Animation.Play(m_ElevatorAnimation.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PowerOn()
    {
        Debug.Log("POWER");
        m_Animation[m_ElevatorAnimation.name].speed = 1;
    }

    public void PowerOff()
    {
        Debug.Log("NOT POWER");
        m_Animation[m_ElevatorAnimation.name].speed = 0;
    }
}
