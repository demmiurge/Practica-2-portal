using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectStateLoadReload : MonoBehaviour, ISaveLoad
{
    public bool m_AvailableToSetAttributes = false;

    public abstract void SetCurrentAttributesAsDefault();

    public abstract void LoadDefaultAttributes();

    public bool CanAttributesBeSet() => m_AvailableToSetAttributes;
}
