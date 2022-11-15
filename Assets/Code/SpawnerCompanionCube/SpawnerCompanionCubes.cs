using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCompanionCubes : MonoBehaviour
{
    public GameObject m_CompanionCubes;
    public Transform m_SpawnPoint;

    public void Spawner()
    {
        m_CompanionCubes.transform.position = m_SpawnPoint.transform.position;
        m_CompanionCubes.transform.rotation = m_SpawnPoint.transform.rotation;
    }
}
