using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStatusSaveLoad : MonoBehaviour
{
    public KeyCode m_RespawnKeyCode = KeyCode.R;
    public KeyCode m_SaveNewPointKeyCode = KeyCode.I;

    public List<GameObject> l_PoolObjectsToRestart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(m_RespawnKeyCode))
            foreach (GameObject l_GameObject in l_PoolObjectsToRestart)
            foreach (ISaveLoad l_ComponentToRestart in l_GameObject.GetComponents<ISaveLoad>())
                l_ComponentToRestart.LoadDefaultAttributes();

        if (Input.GetKey(m_SaveNewPointKeyCode))
            foreach (GameObject l_GameObject in l_PoolObjectsToRestart)
            foreach (ISaveLoad l_ComponentToRestart in l_GameObject.GetComponents<ISaveLoad>())
                if (l_ComponentToRestart.CanAttributesBeSet())
                    l_ComponentToRestart.SetCurrentAttributesAsDefault();
    }
}
