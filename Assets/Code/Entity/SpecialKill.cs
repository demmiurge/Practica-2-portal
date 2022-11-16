using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialKill : MonoBehaviour
{
    public List<GameObject> m_GameObjects;
    public string m_TagNameToSpecialKill = "DeadZoneLaser";
    public float m_TimeToDisappear = 3;

    bool m_Killed = false;
    float m_Current = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Killed) {
            float l_Dissolved = 1.0f;

            float l_Trans = l_Dissolved - m_Current;
            l_Trans *= Time.deltaTime;

            m_Current += l_Trans;

            foreach (GameObject l_GameObject in m_GameObjects)
            {
                l_GameObject.GetComponent<Renderer>().material.SetFloat("_DissolveAmount", m_Current);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == m_TagNameToSpecialKill)
        {
            m_Killed = true;
            StartCoroutine(HideEntity());
        }
    }

    IEnumerator HideEntity()
    {
        yield return new WaitForSeconds(m_TimeToDisappear);
        gameObject.SetActive(false);
        m_Killed = false;
    }
}
