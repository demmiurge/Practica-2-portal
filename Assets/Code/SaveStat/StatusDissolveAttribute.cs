using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatusDissolveAttribute : GameObjectStateLoadReload
{
    float m_StatusDissolve;

    public List<GameObject> m_GameObjects;

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentAttributesAsDefault();
    }

    public override void SetCurrentAttributesAsDefault()
    {
        foreach (GameObject l_GameObject in m_GameObjects)
        {
            m_StatusDissolve = l_GameObject.GetComponent<Renderer>().material.GetFloat("_DissolveAmount");
        }
    }

    public override void LoadDefaultAttributes()
    {
        foreach (GameObject l_GameObject in m_GameObjects)
        {
            l_GameObject.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", m_StatusDissolve);
        }
    }
}
